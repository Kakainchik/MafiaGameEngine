using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WPFApplication.ViewModel;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.SourceInitialized += MainWindow_SourceInitialized;

            this.DataContext = new WindowViewModel(this);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            Core.WindowSizing.WindowInitialized(this);
        }
    }
}