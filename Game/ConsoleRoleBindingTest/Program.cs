using System;
using System.Collections.Generic;
using System.
using GameRoles;
using GameRoles.Roles;

namespace ConsoleRoleBindingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>()
            {
                new Player(new MafiaRole(), "Maf"),
                new Player(new CitizenRole(), "Pea"),
                new Player(new DoctorRole(), "Doc")
            };

            

            Pause();
        }

        private static void R_WasRevived(object sender, ActionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void R_WasKilled(object sender, ActionEventArgs e)
        {
            Console.WriteLine(((Role)sender).Name + " was killed by " + e.Who.Name);
        }

        private static void Pause() => Console.ReadKey(false);
    }
}
