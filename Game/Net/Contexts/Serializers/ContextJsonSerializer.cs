using System.Text.Json;

namespace Net.Contexts.Serializers
{
    public class ContextJsonSerializer
    {
        #region Serialize

        public static byte[] Serialize(Context context)
        {
            JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                IncludeFields = true,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize<Context>(context, options);
            using(MemoryStream ms = new MemoryStream())
            {
                return new byte[0];
            }
        }

        public static int Serialize(Context context, Stream stream)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                JsonSerializer.Serialize(ms, context);
                byte[] contextBytes = ms.ToArray();

                context.Header = new ContextHeader
                {
                    Length = contextBytes.Length
                };

                BinaryWriter bw = new BinaryWriter(stream);

                //The first Int32 (4 bytes) will be the context length
                bw.Write(context.Header.Length);

                //Rewrite the actual message data after header position
                bw.Write(contextBytes);

                //Return position
                return 4 + context.Header.Length;
            }
        }

        #endregion

        #region Deserialize

        public static Context Deserialize(byte[] data)
        {
            //Read 4 bytes and get header
            int length = BitConverter.ToInt32(data, 0);

            using(MemoryStream ms = new MemoryStream(data, 4, length))
            {
                Context context = JsonSerializer.Deserialize<Context>(ms);
                return context;
            }
        }

        public static Context Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            //Read the length (4 bytes)
            int length = br.ReadInt32();

            byte[] data = br.ReadBytes(length);

            using(MemoryStream ms = new MemoryStream(data, 0, data.Length))
            {
                Context context = JsonSerializer.Deserialize<Context>(ms);
                return context;
            }
        }

        #endregion
    }
}