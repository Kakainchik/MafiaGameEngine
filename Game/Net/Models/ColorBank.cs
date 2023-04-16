using System.Reflection;

namespace Net.Models
{
    internal static class ColorBank
    {
        private readonly static FieldInfo[] fi = typeof(ColorBank)
            .GetFields(BindingFlags.NonPublic |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType.IsEquivalentTo(typeof(RGB)))
            .ToArray();

        internal readonly static RGB Crimson = new(0xDC, 0x14, 0x3C);
        internal readonly static RGB Aqua = new(0x00, 0xFF, 0xFF);
        internal readonly static RGB Aquamarine = new(0x7F, 0xFF, 0xD4);
        internal readonly static RGB Blue = new(0x00, 0x01, 0xFF);
        internal readonly static RGB BlueViolet = new(0x8A, 0x2B, 0xE2);
        internal readonly static RGB Brown = new(0xA5, 0x2A, 0x2A);
        internal readonly static RGB BurlyWood = new(0xDE, 0xB8, 0x87);
        internal readonly static RGB CadetBlue = new(0x5F, 0x9E, 0xA0);
        internal readonly static RGB Chartreuse = new(0x7F, 0xFF, 0x00);
        internal readonly static RGB Chocolate = new(0xD2, 0x69, 0x1E);
        internal readonly static RGB Coral = new(0xFF, 0x7F, 0x50);
        internal readonly static RGB Cornflower = new(0x64, 0x95, 0xED);
        internal readonly static RGB DarkBlue = new(0x00, 0x00, 0x8B);
        internal readonly static RGB DarkCyan = new(0x00, 0x8B, 0x8B);
        internal readonly static RGB DarkGoldenRod = new(0xB8, 0x86, 0x0B);
        internal readonly static RGB DarkGreen = new(0x00, 0x64, 0x00);
        internal readonly static RGB DarkKhaki = new(0xBD, 0xB7, 0x6B);
        internal readonly static RGB DarkMagenta = new(0x8B, 0x00, 0x8B);
        internal readonly static RGB DarkOlive = new(0x55, 0x6B, 0x2F);
        internal readonly static RGB DarkOrange = new(0xFF, 0x8C, 0x00);
        internal readonly static RGB DarkOrchid = new(0x99, 0x32, 0xCC);
        internal readonly static RGB DarkRed = new(0x8B, 0x00, 0x00);
        internal readonly static RGB DarkSalmon = new(0xE9, 0x96, 0x7A);
        internal readonly static RGB DarkTurquoise = new(0x00, 0xCE, 0xD1);
        internal readonly static RGB DeepPink = new(0xFF, 0x14, 0x93);
        internal readonly static RGB DodgerBlue = new(0x1E, 0x90, 0xFF);
        internal readonly static RGB FireBrick = new(0xB2, 0x22, 0x22);
        internal readonly static RGB ForestGreen = new(0x22, 0x8B, 0x22);
        internal readonly static RGB Fuchsia = new(0xFF, 0x00, 0xFF);
        internal readonly static RGB Gold = new(0xFF, 0xD7, 0x00);
        internal readonly static RGB GoldenRod = new(0xDA, 0xA5, 0x20);
        internal readonly static RGB Green = new(0x10, 0x99, 0x10);
        internal readonly static RGB GreenYellow = new(0xAD, 0xFF, 0x2F);
        internal readonly static RGB HotPink = new(0xFF, 0x69, 0xB4);
        internal readonly static RGB Indigo = new(0x4B, 0x00, 0x82);
        internal readonly static RGB Khaki = new(0xF0, 0xE6, 0x8C);
        internal readonly static RGB LightBlue = new(0xAD, 0xD8, 0xE6);
        internal readonly static RGB LightSeaGreen = new(0x20, 0xB2, 0xAA);
        internal readonly static RGB Lime = new(0x01, 0xFF, 0x01);
        internal readonly static RGB MediumBlue = new(0x00, 0x00, 0xCD);
        internal readonly static RGB SpringGreen = new(0x00, 0xFA, 0x9A);
        internal readonly static RGB Orange = new(0xFF, 0xA5, 0x00);
        internal readonly static RGB OrangeRed = new(0xFF, 0x45, 0x8C);
        internal readonly static RGB Orchid = new(0xDA, 0x70, 0xD6);
        internal readonly static RGB Red = new(0xFF, 0x00, 0x00);
        internal readonly static RGB Tomato = new(0xFF, 0x63, 0x47);
        internal readonly static RGB Yellow = new(0xFF, 0xFF, 0x00);

        internal static RGB GetRandomColor()
        {
            int i = Random.Shared.Next(fi.Length);
            return (RGB)fi[i].GetValue(null)!;
        }
    }
}