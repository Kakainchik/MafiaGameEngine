using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable SYSLIB0011

namespace Net.Contexts.Serializers
{
    public static class ContextByteSerializer
    {
        #region Serialize

        public static byte[] Serialize(Context context)
        {
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, context);
                byte[] contextBytes = ms.ToArray();

                context.Header = new ContextHeader
                {
                    Length = contextBytes.Length
                };

                using (BufferedStream bs = new BufferedStream(ms, contextBytes.Length + 4))
                using (BinaryWriter bw = new BinaryWriter(bs))
                {
                    //Set position to the begining
                    bw.Seek(0, SeekOrigin.Begin);

                    //The first Int32 (4 bytes) will be the context length
                    bw.Write(context.Header.Length);

                    //Rewrite the actual message data after header position
                    bw.Write(contextBytes);

                    return ms.ToArray();
                }
            }
        }

        public static int Serialize(Context context, Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, context);
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

            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data, 4, length))
            {
                Context context = (Context)formatter.Deserialize(ms);
                return context;
            }
        }

        public static Context Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);

            //Read the length (4 bytes)
            int length = br.ReadInt32();

            byte[] data = br.ReadBytes(length);

            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data, 0, data.Length))
            {
                Context context = (Context)formatter.Deserialize(ms);
                return context;
            }
        }

        #endregion
    }
}

#pragma warning restore SYSLIB1100