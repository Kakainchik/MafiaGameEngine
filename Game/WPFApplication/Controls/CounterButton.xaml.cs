using WPFApplication.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for CounterButton.xaml
    /// </summary>
    public partial class CounterButton : UserControl
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        /// <summary>
        /// The numeric value on the text box.
        /// </summary>
        public int CountValue
        {
            get => (int)GetValue(CountValueProperty);
            set => SetValue(CountValueProperty, value);
        }

        public ICommand ValueChanged
        {
            get => (ICommand)GetValue(ValueChangedProperty);
            set => SetValue(ValueChangedProperty, value);
        }

        public bool IsDownEnabled
        {
            get => (bool)GetValue(CountValueProperty);
            set => SetValue(CountValueProperty, value);
        }

        public ICommand CountUpCommand { get; set; }
        public ICommand CountDownCommand { get; set; }

        public static readonly DependencyProperty CountValueProperty =
            DependencyProperty.Register(nameof(CountValue),
                typeof(int),
                typeof(CounterButton));

        public static readonly DependencyProperty ValueChangedProperty =
            DependencyProperty.Register(nameof(ValueChanged),
                typeof(ICommand),
                typeof(CounterButton));

        public static readonly DependencyProperty IsDownEnabledProperty =
            DependencyProperty.Register(nameof(IsDownEnabled),
                typeof(bool),
                typeof(CounterButton));

        public CounterButton()
        {
            InitializeComponent();

            CountUpCommand = new RelayCommand(o =>
            {
                if(CountValue < MaxValue)
                {
                    CountValue++;
                    ValueChanged?.Execute(CountValue);
                }
            });
            CountDownCommand = new RelayCommand(o =>
            {
                if(CountValue > MinValue)
                {
                    CountValue--;
                    ValueChanged?.Execute(CountValue);
                }
            });
        }
    }
}