using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    public class InputTextBox : TextBox
    {
        public string Hint { get; set; }
        public bool IsBlankAllowed { get; set; } = true;

        public ICommand PressCommand
        {
            get => (ICommand)GetValue(PressCommandProperty);
            set => SetValue(PressCommandProperty, value);
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register(nameof(Hint),
                typeof(string),
                typeof(InputTextBox));

        public static readonly DependencyProperty IsBlankAllowedProperty =
            DependencyProperty.Register(nameof(IsBlankAllowed),
                typeof(bool),
                typeof(InputTextBox),
                new PropertyMetadata(defaultValue: true));

        public static readonly DependencyProperty PressCommandProperty =
            DependencyProperty.Register(nameof(PressCommand),
                typeof(ICommand),
                typeof(InputTextBox));

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.Key == Key.Enter)
            {
                if(PressCommand != null)
                {
                    PressCommand.Execute(base.Text);
                    base.Text = string.Empty;
                }
            }
        }
    }
}
