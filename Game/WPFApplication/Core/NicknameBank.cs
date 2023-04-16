using System;

namespace WPFApplication.Core
{
    internal static class NicknameBank
    {
        private static object _lock = new object();

        private static readonly string[] NicknamesTemplate =
            { "Abramo", "Alfredo", "Alhero", "Anacleto", "Antonino", "Arcangelo",
            "Aura", "Aurelio", "Bartolomeo", "Berenice", "Camilla",
            "Cirillo", "Colomba", "Colombano", "Corona", "Cosmo", "Dalila",
            "Diletta", "Edvige", "Efisio", "Elisabetta", "Ermelinda", "Evelina",
            "Faustina", "Fulvio", "Gennarino", "Geremia", "Gianmaria",
            "Giuseppina", "Greta", "Katia", "Liberatore", "Lidia", "Nazzareno",
            "Nicomede", "Norma", "Panfilo", "Raffaella", "Roberta", "Robertina",
            "Romana", "Romano", "Selvaggia", "Stella", "Tonio", "Zolegen" };

        private static readonly string[] GamingNicknamesTemplate =
            { "TermoBoy", "OptimusCrime", "Havana", "Terrorist", "MegaMask",
            "Joker", "PackMan", "WarMan", "Gargo", "Vergingo", "Caramello",
            "ZeroBrain", "OnixGuy", "Emil", "FarSeer", "LavaDrink",
            "RetroPower", "Eleonora", "CrazyFrog", "LemonFun", "JojoMark",
            "ScaryBug", "SweetBlood", "AntWood", "ArmyOfLovers", "UglyDog",
            "MarcoPolo", "NeedForSleep", "NewPlayer", "Xerox", "Tomatos",
            "ProNoob", "Himtech", "EroticCat", "FantoMask", "Gidrolog",
            "Malagor", "Lomneras", "Brerkog", "Pereaf", "Gerbri", "Jerarko",
            "EmptyBrain", "Klemid", "Ywador", "Kreaver", "Veraver", "Pedros",
            "OrgasmicBob", "Martyn", "Kebab", "CallToMyMom", "AlfaMain",
            "Qwerty", "NecroFill", "Minority" };

        internal static string GetRandomName(NameTemplate template)
        {
            switch(template)
            {
                case NameTemplate.NATURAL:
                    return NicknamesTemplate[Random.Shared.Next(NicknamesTemplate.Length)];
                case NameTemplate.GAMING:
                    return GamingNicknamesTemplate[Random.Shared.Next(GamingNicknamesTemplate.Length)];
                default:
                    throw new ArgumentException("Template is not valid", nameof(template));
            }
        }
    }

    public enum NameTemplate
    {
        NATURAL,
        GAMING
    }
}