using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRoles.Roles
{
    public class VigilanteRole : Role
    {
        private int qBullets;

        public int QuantityBullet { get => qBullets; }

        public VigilanteRole(int bullets) : base()
        {
            this.qBullets = bullets;
        }

        public override void DoAction()
        {

        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
