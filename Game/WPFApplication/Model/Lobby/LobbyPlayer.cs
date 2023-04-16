using System;
using WPFApplication.Core;

namespace WPFApplication.Model
{
    public class LobbyPlayer : ObservableObject
    {
        private bool isReady;

        public ulong PlayerId { get; }
        public string Username { get; }

        public bool IsReady
        {
            get => isReady;
            set
            {
                isReady = value;
                OnPropertyChanged(nameof(IsReady));
            }
        }

        public LobbyPlayer(ulong id, string username)
        {
            PlayerId = id;
            Username = username;
        }

        public LobbyPlayer(ulong id, string username, bool isReady) : this(id, username)
        {
            this.isReady = isReady;
        }
    }
}