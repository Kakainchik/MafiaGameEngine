﻿using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class DayPlayerStateContext : SessionContext
    {
        public ulong Id { get; }
        public string Nickname { get; }
        public RGB NColor { get; }
        public bool IsAlive { get; }

        [JsonConstructor]
        public DayPlayerStateContext(ulong id,
            string nickname,
            RGB nColor,
            bool isAlive)
        {
            Id = id;
            Nickname = nickname;
            IsAlive = isAlive;
            NColor = nColor;
        }
    }
}