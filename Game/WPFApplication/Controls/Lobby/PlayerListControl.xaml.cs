using WPFApplication.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for PlayerListControl.xaml
    /// </summary>
    public partial class PlayerListControl : UserControl
    {
        public IEnumerable<LobbyPlayer> Players
        {
            get => (IEnumerable<LobbyPlayer>)GetValue(PlayersProperty);
            set => SetValue(PlayersProperty, value);
        }

        public static readonly DependencyProperty PlayersProperty =
            DependencyProperty.Register(nameof(Players),
                typeof(IEnumerable<LobbyPlayer>),
                typeof(PlayerListControl));

        public PlayerListControl()
        {
            InitializeComponent();
        }
    }
}