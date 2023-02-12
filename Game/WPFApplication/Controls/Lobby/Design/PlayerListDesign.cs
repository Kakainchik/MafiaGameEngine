using System.Collections.Generic;

namespace WPFApplication.Controls.Lobby.Design
{
    internal class PlayerListDesign
    {
        public IEnumerable<string> Players { get; set; }

        public PlayerListDesign()
        {
            Players = new List<string>()
            {
                "Kakainchik",
                "Shamal1999",
                "DarkPredator",
                "Qumanqul"
            };
        }
    }
}