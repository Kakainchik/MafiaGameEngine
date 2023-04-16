using WPFApplication.Core;
using WPFApplication.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for MutableRoleListItemControl.xaml
    /// </summary>
    public partial class MutableRoleListItemControl : UserControl
    {
        public RoleVisual Role
        {
            get => (RoleVisual)GetValue(RoleProperty);
            set => SetValue(RoleProperty, value);
        }

        public int Quantity
        {
            get => (int)GetValue(QuantityProperty);
            set => SetValue(QuantityProperty, value);
        }

        public bool IsDownEnabled
        {
            get => (bool)GetValue(IsDownEnabledProperty);
            set => SetValue(IsDownEnabledProperty, value);
        }

        public bool IsUpEnabled
        {
            get => (bool)GetValue(IsUpEnabledProperty);
            set => SetValue(IsUpEnabledProperty, value);
        }

        public ICommand QuantityChange
        {
            get => (ICommand)GetValue(QuantityChangeProperty);
            set => SetValue(QuantityChangeProperty, value);
        }

        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }

        public static readonly DependencyProperty RoleProperty =
            DependencyProperty.Register("Role",
                typeof(RoleVisual),
                typeof(MutableRoleListItemControl));

        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register("Quantity",
                typeof(int),
                typeof(MutableRoleListItemControl));

        public static readonly DependencyProperty IsDownEnabledProperty =
            DependencyProperty.Register("IsDownEnabled",
                typeof(bool),
                typeof(MutableRoleListItemControl));

        public static readonly DependencyProperty IsUpEnabledProperty =
            DependencyProperty.Register("IsUpEnabled",
                typeof(bool),
                typeof(MutableRoleListItemControl),
                new PropertyMetadata(true));

        public static readonly DependencyProperty QuantityChangeProperty =
            DependencyProperty.Register("QuantityChange",
                typeof(ICommand),
                typeof(MutableRoleListItemControl));

        public MutableRoleListItemControl()
        {
            InitializeComponent();
            UpCommand = new RelayCommand(OnUp);
            DownCommand = new RelayCommand(OnDown);
        }

        private void OnUp(object? obj)
        {
            Quantity++;
            if(Quantity > 0) IsDownEnabled = true;

            QuantityChange?.Execute((Role, Quantity));
        }

        private void OnDown(object? obj)
        {
            Quantity--;
            if(Quantity == 0) IsDownEnabled = false;

            QuantityChange?.Execute(new ValueTuple<RoleVisual, int>(Role, Quantity));
        }
    }
}