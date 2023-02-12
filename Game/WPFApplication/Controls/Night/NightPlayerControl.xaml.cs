using WPFApplication.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for NightPlayerControl.xaml
    /// </summary>
    public partial class NightPlayerControl : UserControl
    {
        public NightPlayerState Player
        {
            get => (NightPlayerState)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        public static readonly DependencyProperty PlayerProperty =
            DependencyProperty.Register(nameof(Player),
                typeof(NightPlayerState),
                typeof(NightPlayerControl));

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(nameof(ClickCommand),
                typeof(ICommand),
                typeof(NightPlayerControl));

        public NightPlayerControl()
        {
            InitializeComponent();
        }
    }
}