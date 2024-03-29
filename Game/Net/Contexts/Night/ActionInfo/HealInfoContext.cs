﻿using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class HealInfoContext : NightInfoContext
    {
        public bool ToTarget { get; }

        [JsonConstructor]
        public HealInfoContext(bool toTarget)
        {
            ToTarget = toTarget;
        }
    }
}