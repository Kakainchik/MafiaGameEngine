using WPFApplication.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for TargetPickerControl.xaml
    /// </summary>
    public partial class TargetPickerControl : UserControl
    {
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public int TargetNumber
        {
            get => (int)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public NightPlayerState Target
        {
            get => (NightPlayerState)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(nameof(IsActive),
                typeof(bool),
                typeof(TargetPickerControl));

        public static readonly DependencyProperty TargetNumberProperty =
            DependencyProperty.Register(nameof(TargetNumber),
                typeof(int),
                typeof(TargetPickerControl));

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(nameof(Target),
                typeof(NightPlayerState),
                typeof(TargetPickerControl));

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                typeof(ICommand),
                typeof(TargetPickerControl));

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(nameof(ClickCommand),
                typeof(ICommand),
                typeof(TargetPickerControl));

        public TargetPickerControl()
        {
            InitializeComponent();
        }
    }
}
