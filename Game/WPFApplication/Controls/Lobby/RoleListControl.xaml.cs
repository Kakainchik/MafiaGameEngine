using WPFApplication.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFApplication.Controls
{
    /// <summary>
    /// Interaction logic for RoleListControl.xaml
    /// </summary>
    public partial class RoleListControl : UserControl
    {
        public IDictionary<RoleVisual, int> SelectedRoles
        {
            get => (IDictionary<RoleVisual, int>)GetValue(SelectedRolesProperty);
            set => SetValue(SelectedRolesProperty, value);
        }

        public static readonly DependencyProperty RolesProperty =
            DependencyProperty.Register("Roles",
                typeof(IEnumerable<RoleVisual>),
                typeof(RoleListControl));

        public static readonly DependencyProperty SelectedRolesProperty =
            DependencyProperty.Register("SelectedRoles",
                typeof(IDictionary<RoleVisual, int>),
                typeof(RoleListControl),
                new PropertyMetadata(new Dictionary<RoleVisual, int>()));

        public RoleListControl()
        {
            InitializeComponent();
        }
    }
}