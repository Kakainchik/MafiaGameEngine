using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Contexts;
using Net.Contexts.Connection;
using Net.Contexts.Intro;
using Net.Contexts.Lobby;
using Net.Contexts.Serializers;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApplicationTest
{
    [TestClass]
    public class ContextByteSerializationTest
    {
#pragma warning disable SYSLIB0011

        [TestMethod]
        public void CompareSerializing_AppendHeader_DifferentSize()
        {
            IntroContext message = new IntroContext(IntroStep.START);

            byte[] data = ContextByteSerializer.Serialize(message);

            IFormatter formatter = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, message);

            Assert.AreNotEqual(ms.Length, data.LongLength);
        }

        [TestMethod]
        public void SerializeContext_DesirializeBytes_HasSameData()
        {
            Context expected = new IntroContext(IntroStep.END);

            byte[] data = ContextByteSerializer.Serialize(expected);

            var message = ContextByteSerializer.Deserialize(data);

            Assert.IsInstanceOfType(message, typeof(IntroContext));
            Assert.AreEqual(IntroStep.END, ((IntroContext)message).Step);
        }

        [TestMethod]
        [DataRow("Bob")]
        [DataRow("Clark")]
        public void SerializeContext_InStream_HasSameData(string username)
        {
            UsernameContext expected = new UsernameContext(username);

            using(MemoryStream ms = new MemoryStream())
            {
                int bytesWritten = ContextByteSerializer.Serialize(expected, ms);

                ms.Position -= bytesWritten;

                var message = ContextByteSerializer.Deserialize(ms);

                Assert.IsInstanceOfType(message, typeof(UsernameContext));
                Assert.AreEqual(expected.Username, ((UsernameContext)message).Username);
            }
        }

        [TestMethod]
        public void SerializeManyContexts_RandomData_EachContextDeserialized()
        {
            Random ran = new Random();

            LobbyMaxPlayerContext[] contexts = new LobbyMaxPlayerContext[10];
            for(int i = 0; i < contexts.Length; i++)
            {
                contexts[i] = new LobbyMaxPlayerContext(ran.Next());
            }

            using MemoryStream ms = new MemoryStream();
            for(int i = 0; i < contexts.Length; i++)
            {
                ContextByteSerializer.Serialize(contexts[i], ms);
            }

            ms.Position = 0;
            for(int i = 0; i < contexts.Length; i++)
            {
                LobbyMaxPlayerContext temp = 
                    ContextByteSerializer.Deserialize(ms) as LobbyMaxPlayerContext;

                Assert.IsInstanceOfType(temp, typeof(LobbyMaxPlayerContext));
                Assert.AreEqual(temp.Quantity, contexts[i].Quantity);
            }
        }

        [TestMethod]
        public void SerializeByte_DeserializeStream_TheSameData()
        {
            SessionIdContext sent = new SessionIdContext(Guid.Empty);
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            byte[] data = ContextByteSerializer.Serialize(sent);
            bw.Write(data);

            ms.Position -= data.Length;

            SessionIdContext result = (SessionIdContext)ContextByteSerializer.Deserialize(ms);

            Assert.AreEqual(sent.Id, result.Id);
        }

#pragma warning restore SYSLIB0011
    }
}