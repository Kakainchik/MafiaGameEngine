using System.Windows;
using System.Windows.Controls;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for PlayerListItemControl.xaml
    /// </summary>
    public partial class PlayerListItemControl : UserControl
    {
        public string Player
        {
            get => (string)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }

        public bool IsReady
        {
            get => (bool)GetValue(IsReadyProperty);
            set => SetValue(IsReadyProperty, value);
        }

        public static readonly DependencyProperty PlayerProperty =
            DependencyProperty.Register(nameof(Player),
                typeof(string),
                typeof(PlayerListItemControl));

        public static readonly DependencyProperty IsReadyProperty =
            DependencyProperty.Register(nameof(IsReady),
                typeof(bool),
                typeof(PlayerListItemControl));

        public PlayerListItemControl()
        {
            InitializeComponent();
        }
    }
}