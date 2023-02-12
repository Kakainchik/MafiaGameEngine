using WPFApplication.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for ColoredChatListControl.xaml
    /// </summary>
    public partial class ColoredChatListControl : UserControl
    {
        public ObservableCollection<ColoredChatMessage> Messages
        {
            get => (ObservableCollection<ColoredChatMessage>)GetValue(MessagesProperty);
            set => SetValue(MessagesProperty, value);
        }

        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register(nameof(Messages),
                typeof(ObservableCollection<ColoredChatMessage>),
                typeof(ColoredChatListControl),
                new PropertyMetadata(null));

        public ColoredChatListControl()
        {
            InitializeComponent();
        }
    }
}