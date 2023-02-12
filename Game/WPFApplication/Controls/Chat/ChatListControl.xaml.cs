using WPFApplication.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for ChatListControl.xaml
    /// </summary>
    public partial class ChatListControl : UserControl
    {
        public ObservableCollection<ChatMessage> Messages
        {
            get => (ObservableCollection<ChatMessage>)GetValue(MessagesProperty);
            set => SetValue(MessagesProperty, value);
        }

        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register(nameof(Messages),
                typeof(ObservableCollection<ChatMessage>),
                typeof(ChatListControl),
                new PropertyMetadata(null));

        public ChatListControl()
        {
            InitializeComponent();
        }
    }
}