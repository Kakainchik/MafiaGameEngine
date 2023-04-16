using System;
using System.Windows.Media;

namespace WPFApplication.Model
{
    public class ColoredChatMessage : ChatMessage
    {
        public Brush ColourBrush { get; set; }

        public ColoredChatMessage(Color color, string username, string message)
            : base(username, message)
        {
            ColourBrush = new SolidColorBrush(color);
        }

        public ColoredChatMessage(Color color, string username, string message, DateTime time)
            : base(username, message, time)
        {
            ColourBrush = new SolidColorBrush(color);
        }
    }
}