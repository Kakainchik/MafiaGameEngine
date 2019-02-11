using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRoles.Interfaces
{
    public interface IAction
    {
        bool Execute(Role who);
        void Undo();
    }
}
