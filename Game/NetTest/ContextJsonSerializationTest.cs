using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Contexts.Connection;
using Net.Contexts.Intro;
using Net.Contexts.Lobby;
using Net.Contexts.Serializers;
using Net.Contexts;
using System;
using System.IO;
using Net.Contexts.Game;
using Net.Models;
using GameLogic.Model;

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
            ConnectUsernameContext expected = new ConnectUsernameContext(username);

            using(MemoryStream ms = new MemoryStream())
            {
                int bytesWritten = ContextJsonSerializer.Serialize(expected, ms);

                ms.Position -= bytesWritten;

                var message = ContextJsonSerializer.Deserialize(ms);

                Assert.IsInstanceOfType(message, typeof(ConnectUsernameContext));
                Assert.AreEqual(expected.Username, ((ConnectUsernameContext)message).Username);
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
                ContextJsonSerializer.Serialize(contexts[i], ms);
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
            ConnectClientIdContext sent = new ConnectClientIdContext(1UL);
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            byte[] data = ContextJsonSerializer.Serialize(sent);
            bw.Write(data);

            ms.Position -= data.Length;

            ConnectClientIdContext result = (ConnectClientIdContext)ContextJsonSerializer.Deserialize(ms);

            Assert.AreEqual(sent.ClientId, result.ClientId);
        }

        [TestMethod]
        public void SerializeContext_EndGameContext_HasSameData()
        {
            EndGamePlayerState[] playerStates =
            {
                new EndGamePlayerState(0UL, "Nickname1", new RGB(10, 10, 10), RoleSignature.CITIZEN, true),
                new EndGamePlayerState(0UL, "Nickname2", new RGB(20, 30, 40), RoleSignature.MAFIA, false)
            };
            EndGameNightH[] nightActions =
            {
                new EndGameNightH("Citizen", RoleSignature.CITIZEN, "A Mafia Target", true)
            };
            EndGameHistory[] cycles =
            {
                new EndGameHistory(1, "Nickname2", 2, "I won", nightActions, new[] { "Zero" })
            };
            EndGameContext expected = new EndGameContext(Team.UNDEAD,
                playerStates,
                cycles);

            byte[] data = ContextJsonSerializer.Serialize(expected);

            var message = ContextJsonSerializer.Deserialize(data);

            Assert.IsInstanceOfType(expected, typeof(EndGameContext));
            Assert.AreEqual(expected.Winner, ((EndGameContext)message).Winner);
        }
    }
}