using System;
using System.Windows;
using System.Windows.Controls;

#pragma warning disable CS8618

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for ChatListItemControl.xaml
    /// </summary>
    public partial class ChatListItemControl : UserControl
    {
        public DateTime MessageTime { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public static DependencyProperty MessageTimeProperty =
            DependencyProperty.Register(
                nameof(MessageTime),
                typeof(DateTime),
                typeof(ChatListItemControl));

        public static DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(string),
                typeof(ChatListItemControl));

        public static DependencyProperty UsernameProperty =
            DependencyProperty.Register(
                nameof(Username),
                typeof(string),
                typeof(ChatListItemControl));

        public ChatListItemControl()
        {
            InitializeComponent();
        }
    }
}