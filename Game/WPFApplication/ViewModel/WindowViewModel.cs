using WPFApplication.Core;
using System.Windows;
using System.Windows.Input;

namespace WPFApplication.ViewModel
{
    /// <summary>
    /// A ViewModel of the main window of application.
    /// </summary>
    public class WindowViewModel : ObservableObject, IPageHost
    {
        private Window mWindow;
        private object currentView;
        private bool isMenuEnabled = true;

        #region Properties

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand HomeViewCommand { get; set; }
        public ICommand SettingsViewCommand { get; set; }
        public ICommand InfoViewCommand { get; set; }

        public SettingsViewModel SettingsVM { get; set; }
        public InfoViewModel InfoVM { get; set; }
        public ChangeablePage HomeVMRepresentation { get; set; }

        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public bool IsMenuEnabled
        {
            get => isMenuEnabled;
            set
            {
                isMenuEnabled = value;
                OnPropertyChanged(nameof(IsMenuEnabled));
            }
        }

        #endregion

        //Constructor
        public WindowViewModel(Window window)
        {
            mWindow = window;
            SettingsVM = new SettingsViewModel();
            InfoVM = new InfoViewModel();
            HomeVMRepresentation = new HomeViewModel() { Successor = this };

            currentView = HomeVMRepresentation;

            //CreateCommands
            MinimizeCommand = new RelayCommand(OnMinimizeWindow);
            MaximizeCommand = new RelayCommand(OnMaximizeWindow);
            CloseCommand = new RelayCommand(OnCloseWindow);
            HomeViewCommand = new RelayCommand(OnHomeView);
            SettingsViewCommand = new RelayCommand(OnSettingsView);
            InfoViewCommand = new RelayCommand(OnInfoView);
        }

        public void AssertPage(ChangeablePage page)
        {
            //When the view goes to the next page
            CurrentView = page;
            IsMenuEnabled = false;
        }

        private void OnHomeView(object? o)
        {
            if(CurrentView is INetHolder inh)
            {
                inh.AbortConnections();
            }
            else if(CurrentView is INetUser inu)
            {
                inu.NetHolder?.AbortConnections();
            }

            //Show main page
            CurrentView = HomeVMRepresentation;
            IsMenuEnabled = true;
        }

        private void OnSettingsView(object? o)
        {
            CurrentView = SettingsVM;
        }

        private void OnInfoView(object? o)
        {
            CurrentView = InfoVM;
        }

        protected virtual void OnMinimizeWindow(object? parameter)
        {
            mWindow.WindowState = WindowState.Minimized;
        }

        protected virtual void OnMaximizeWindow(object? parameter)
        {
            mWindow.WindowState ^= WindowState.Maximized;
        }

        protected virtual void OnCloseWindow(object? parameter)
        {
            mWindow.Close();
            //TODO: If a game is running - warning
        }
    }
}