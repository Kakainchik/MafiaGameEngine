using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRoles
{
    public class Player
    {
        private string nickname;
        private string deadnote;
        private Role gameRole;
        private static byte id;

        public Player(Role role, string nickname, string note = null)
        {
            gameRole = role;
        }

        public void SaveState()
        {

        }

        public void RestoreState()
        {

        }
    }
}
