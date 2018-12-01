using System;
using System.Collections.Generic;
using GameRoles;
using GameRoles.Roles;

namespace ConsoleRoleBindingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Role> players = new List<Role>(2)
            {
                new CitizenRole("Camel"),
                new MafiaRole("Job")
            };

            foreach(Role r in players)
            {
                r.WasDied += new ActionHandler(R_WasDied);
                r.WasRevived += new ActionHandler(R_WasRevived);
            }

            players.ForEach((r) => Console.WriteLine(r.ToString()));

            players[1].ExecuteAction(players[0]);

            Pause();
        }

        private static void R_WasRevived(object sender, ActionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void R_WasDied(object sender, ActionEventArgs e)
        {
            Console.WriteLine(((Role)sender).Name + " was killed by " + e.Who.Name);
        }

        private static void Pause() => Console.ReadKey(false);
    }
}
