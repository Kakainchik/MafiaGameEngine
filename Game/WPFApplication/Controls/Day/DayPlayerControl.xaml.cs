using WPFApplication.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for DayPlayerControl.xaml
    /// </summary>
    public partial class DayPlayerControl : UserControl
    {
        public DayPlayerState Player
        {
            get => (DayPlayerState)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        public static readonly DependencyProperty PlayerProperty =
            DependencyProperty.Register(nameof(Player),
                typeof(DayPlayerState),
                typeof(DayPlayerControl));

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(nameof(ClickCommand),
                typeof(ICommand),
                typeof(DayPlayerControl));

        public DayPlayerControl()
        {
            InitializeComponent();
        }
    }
}