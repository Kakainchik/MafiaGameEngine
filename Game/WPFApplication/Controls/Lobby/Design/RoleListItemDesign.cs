using WPFApplication.Model;

namespace WPFApplication.Controls.Lobby.Design
{
    internal class RoleListItemDesign
    {
        public RoleVisual Role { get; set; }
        public int Quantity { get; set; }

        public RoleListItemDesign()
        {
            Role = RoleVisual.DETECTIVE;
            Quantity = 0;
        }
    }
}