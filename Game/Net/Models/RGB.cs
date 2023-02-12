namespace Net.Models
{
    [Serializable]
    public struct RGB
    {
        public byte R;
        public byte G;
        public byte B;

        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}