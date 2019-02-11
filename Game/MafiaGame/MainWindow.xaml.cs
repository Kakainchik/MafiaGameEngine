using MafiaGame.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace MafiaGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowViewModel(this);
        }
    }
}