using System.Windows;
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

        private void MainWindow_SourceInitialized(object sender, System.EventArgs e)
        {
            Core.WindowSizing.WindowInitialized(this);
        }
    }
}