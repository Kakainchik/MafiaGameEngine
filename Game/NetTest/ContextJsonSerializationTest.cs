using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Contexts.Connection;
using Net.Contexts.Intro;
using Net.Contexts.Lobby;
using Net.Contexts.Serializers;
using Net.Contexts;
using System;
using System.IO;

namespace NetTest
{
    [TestClass]
    public class ContextJsonSerializationTest
    {
        [TestMethod]
        public void SerializeContext_DesirializeBytes_HasSameData()
        {
            Context expected = new IntroContext(IntroStep.END);

            byte[] data = ContextJsonSerializer.Serialize(expected);

            var message = ContextJsonSerializer.Deserialize(data);

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
                int bytesWritten = ContextJsonSerializer.Serialize(expected, ms);

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
                    ContextJsonSerializer.Deserialize(ms) as LobbyMaxPlayerContext;

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

            byte[] data = ContextJsonSerializer.Serialize(sent);
            bw.Write(data);

            ms.Position -= data.Length;

            SessionIdContext result = (SessionIdContext)ContextJsonSerializer.Deserialize(ms);

            Assert.AreEqual(sent.Id, result.Id);
        }
    }
}