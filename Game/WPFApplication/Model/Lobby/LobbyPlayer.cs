using System;
using WPFApplication.Core;

namespace WPFApplication.Model
{
    public class LobbyPlayer : ObservableObject
    {
        private bool isReady;

        public Guid PlayerId { get; }
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

        public LobbyPlayer(Guid id, string username)
        {
            PlayerId = id;
            Username = username;
        }

        public LobbyPlayer(Guid id, string username, bool isReady) : this(id, username)
        {
            this.isReady = isReady;
        }
    }
}