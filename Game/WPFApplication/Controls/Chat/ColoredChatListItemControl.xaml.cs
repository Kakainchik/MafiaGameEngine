using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#pragma warning disable CS8618

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for ScopedChatListControl.xaml
    /// </summary>
    public partial class ColoredChatListItemControl : UserControl
    {
        public DateTime MessageTime { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public Brush ColourBrush { get; set; }

        public static DependencyProperty MessageTimeProperty =
            DependencyProperty.Register(
                nameof(MessageTime),
                typeof(DateTime),
                typeof(ColoredChatListItemControl));

        public static DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(string),
                typeof(ColoredChatListItemControl));

        public static DependencyProperty UsernameProperty =
            DependencyProperty.Register(
                nameof(Username),
                typeof(string),
                typeof(ColoredChatListItemControl));

        public static readonly DependencyProperty ColourBrushProperty =
            DependencyProperty.Register(nameof(ColourBrush),
                typeof(Brush),
                typeof(ColoredChatListItemControl));

        public ColoredChatListItemControl()
        {
            InitializeComponent();
        }
    }
}