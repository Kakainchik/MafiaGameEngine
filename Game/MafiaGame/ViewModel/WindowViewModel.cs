using MafiaGame.DataModels;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace MafiaGame.ViewModel
{
    /// <summary>
    /// Thw view model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Members

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The radius of the window
        /// </summary>
        private int mWindowRadius = 10;

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// The minimum width of the window
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 900;

        /// <summary>
        /// The mimimum height of the window
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 600;

        /// <summary>
        /// The size of the resize border the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The size oof resize border around the window, taking into accout the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get => new Thickness(ResizeBorder + OuterMatginSize); }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMatginSize
        {
            get => mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            set => mOuterMarginSize = value;
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMatginSizeThickness { get => new Thickness(OuterMatginSize); }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get => new Thickness(ResizeBorder); }

        /// <summary>
        /// The radius of the edge of the window
        /// </summary>
        public int WindowRadius
        {
            get => mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            set => mWindowRadius = value;
        }

        /// <summary>
        /// The radius of the edge of the window
        /// </summary>
        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength { get => new GridLength(TitleHeight + ResizeBorder); }

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        #endregion

        //Constructor
        public WindowViewModel(Window window)
        {
            this.mWindow = window;

            //Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                //Fire off events for all properties that are affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMatginSize));
                OnPropertyChanged(nameof(OuterMatginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            //Create commands
            MinimizeCommand = new RelayComand(OnMinimizeWindow);
            MaximizeCommand = new RelayComand(OnMaximizeWindow);
            CloseCommand = new RelayComand(OnCloseWindow);
            MenuCommand = new RelayComand(OnMenuWindow);
        }

        protected virtual void OnMenuWindow()
        {
            SystemCommands.ShowSystemMenu(mWindow, GetMousePosition());
        }

        protected virtual void OnCloseWindow()
        {
            mWindow.Close();
        }

        protected virtual void OnMaximizeWindow()
        {
            mWindow.WindowState ^= WindowState.Maximized;
        }

        protected virtual void OnMinimizeWindow()
        {
            mWindow.WindowState = WindowState.Minimized;
        }

        #region Helpers

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        }

        /// <summary>
        /// Get the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        #endregion
    }
}