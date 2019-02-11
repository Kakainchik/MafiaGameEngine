using MafiaGame.Pages;
using System.Windows.Controls;

namespace MafiaGame
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : BasePage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void JoinButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AnimateOut();
        }
    }
}