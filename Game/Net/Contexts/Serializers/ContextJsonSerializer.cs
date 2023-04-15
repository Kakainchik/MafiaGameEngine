using System.Text;
using System.Text.Json;

namespace Net.Contexts.Serializers
{
    public static class ContextJsonSerializer
    {
        private readonly static JsonSerializerOptions serializerOptions;

        static ContextJsonSerializer()
        {
            serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                IncludeFields = true,
                WriteIndented = true,
                TypeInfoResolver = new PolymorphicContextTypeResolver()
            };
        }

        #region Serialize

        public static byte[] Serialize(Context context)
        {
            string json = JsonSerializer.Serialize<Context>(context, serializerOptions);

            context.Header = new ContextHeader
            {
                Length = Encoding.UTF8.GetByteCount(json)
            };

            using MemoryStream ms = new MemoryStream(context.Header.Length + 4);
            using BinaryWriter bw = new BinaryWriter(ms);

            //The first Int32 (4 bytes) will be the context length
            bw.Write(BitConverter.GetBytes(context.Header.Length));
            //Rewrite the actual message data after header position
            bw.Write(Encoding.UTF8.GetBytes(json));

            return ms.ToArray();
        }

        public static int Serialize(Context context, Stream stream)
        {
            string json = JsonSerializer.Serialize<Context>(context, serializerOptions);

            context.Header = new ContextHeader
            {
                Length = Encoding.UTF8.GetByteCount(json)
            };

            byte[] contextBytes = Encoding.UTF8.GetBytes(json);

            using BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, true);

            //The first Int32 (4 bytes) will be the context length
            bw.Write(context.Header.Length);
            //Rewrite the actual message data after header position
            bw.Write(contextBytes);

            //Return position
            return 4 + context.Header.Length;
        }

        #endregion

        #region Deserialize

        public static Context Deserialize(byte[] data)
        {
            //Read the length (4 bytes)
            int length = BitConverter.ToInt32(data, 0);
            using(MemoryStream ms = new MemoryStream(data, 4, length))
            {
                byte[] chunk = new byte[length];
                ms.Read(chunk);
                Utf8JsonReader jsonReader = new Utf8JsonReader(chunk);

                Context context = JsonSerializer.Deserialize<Context>(ref jsonReader, serializerOptions)!;
                context.Header = new ContextHeader
                {
                    Length = length
                };

                return context;
            }
        }

        public static Context Deserialize(Stream stream)
        {
            using BinaryReader br = new BinaryReader(stream, Encoding.UTF8, true);

            //Read the length (4 bytes)
            int length = br.ReadInt32();

            byte[] chunk = new byte[length];
            br.Read(chunk, 0, length);
            Utf8JsonReader jsonReader = new Utf8JsonReader(chunk);
            
            Context context = JsonSerializer.Deserialize<Context>(ref jsonReader, serializerOptions)!;
            context.Header = new ContextHeader
            {
                Length = length
            };

            return context;
        }

        #endregion
    }
}