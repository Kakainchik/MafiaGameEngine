using GameLogic.Roles;
using System;

namespace GameLogic
{
    public delegate void LynchEventHandler(object sender, string lastMessage);
    public delegate void KillEventHandler(object sender, string lastMessage, KillEventArgs e);

    public class Player
    {
        public bool isAlive = true;

        public Role Role { get; }
        public string Nickname { get; }
        public int Votes { get; private set; } = 0;
        public bool IsAlive { get => isAlive; }
        public string LastMessage { get; set; }

        public Player(Role role, string nickname)
        {
            this.Role = role;
            this.Nickname = nickname;
        }

        public event KillEventHandler WasKilled;
        public event LynchEventHandler WasLynced;

        public void Lynch()
        {
            this.WasLynced?.Invoke(this, this.LastMessage);
            this.isAlive = false;
        }

        public void Kill(Player killer)
        {
            this.WasKilled?.Invoke(this, this.LastMessage, new KillEventArgs(killer.Role));
            this.isAlive = false;
        }

        public void Vote(Player target) => target.AddVote();

        public void AddVote() => Votes++;

        public void ClearVotes() => Votes = 0;
    }
}