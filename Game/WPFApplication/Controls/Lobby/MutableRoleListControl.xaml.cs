using WPFApplication.Core;
using WPFApplication.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for MutableRoleListControl.xaml
    /// </summary>
    public partial class MutableRoleListControl : UserControl
    {
        public IEnumerable<RoleVisual> Roles
        {
            get => (IEnumerable<RoleVisual>)GetValue(RolesProperty);
            set => SetValue(RolesProperty, value);
        }

        public IDictionary<RoleVisual, int> SelectedRoles
        {
            get => (IDictionary<RoleVisual, int>)GetValue(SelectedRolesProperty);
            set => SetValue(SelectedRolesProperty, value);
        }

        public ICommand SetupChange
        {
            get => (ICommand)GetValue(SetupChangeProperty);
            set => SetValue(SetupChangeProperty, value);
        }

        public ICommand QuantityChangeCommand { get; set; }

        public static readonly DependencyProperty RolesProperty =
            DependencyProperty.Register("Roles",
                typeof(IEnumerable<RoleVisual>),
                typeof(MutableRoleListControl));

        public static readonly DependencyProperty SelectedRolesProperty =
            DependencyProperty.Register("SelectedRoles",
                typeof(IDictionary<RoleVisual, int>),
                typeof(MutableRoleListControl),
                new PropertyMetadata(new Dictionary<RoleVisual, int>()));

        public static readonly DependencyProperty SetupChangeProperty =
            DependencyProperty.Register("SetupChange",
                typeof(ICommand),
                typeof(MutableRoleListControl));

        public MutableRoleListControl()
        {
            InitializeComponent();

            QuantityChangeCommand = new RelayCommand(OnQuantityChange);
        }

        private void OnQuantityChange(object obj)
        {
            var data = (ValueTuple<RoleVisual, int>)obj;
            if(data.Item2 == 0) SelectedRoles.Remove(data.Item1);
            else SelectedRoles[data.Item1] = data.Item2;

            SetupChange?.Execute(data.Item1);
        }
    }
}