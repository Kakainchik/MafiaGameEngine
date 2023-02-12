using WPFApplication.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for HostPlayerListControl.xaml
    /// </summary>
    public partial class HostPlayerListControl : UserControl
    {
        public IEnumerable<LobbyPlayer> Players
        {
            get => (IEnumerable<LobbyPlayer>)GetValue(PlayersProperty);
            set => SetValue(PlayersProperty, value);
        }

        public ICommand PlayerKicked
        {
            get => (ICommand)GetValue(PlayerKickedProperty);
            set => SetValue(PlayerKickedProperty, value);
        }

        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register(nameof(Players),
                typeof(IEnumerable<LobbyPlayer>),
                typeof(HostPlayerListControl));

        public static readonly DependencyProperty PlayerKickedProperty =
            DependencyProperty.Register(nameof(PlayerKicked),
                typeof(ICommand),
                typeof(HostPlayerListControl));

        public HostPlayerListControl()
        {
            InitializeComponent();
        }
    }
}