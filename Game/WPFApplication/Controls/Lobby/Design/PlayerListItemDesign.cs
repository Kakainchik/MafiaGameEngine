namespace WPFApplication.Controls.Lobby.Design
{
    internal class PlayerListItemDesign
    {
        public string Player { get; set; }
        public bool IsReady { get; }

        public PlayerListItemDesign()
        {
            Player = "Kakainchik";
            IsReady = true;
        }
    }
}