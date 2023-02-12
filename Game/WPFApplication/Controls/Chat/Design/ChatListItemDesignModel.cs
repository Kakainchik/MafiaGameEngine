using System;

namespace WPFApplication.Controls.Chat.Design
{
    internal class ChatListItemDesignModel
    {
        public DateTime MessageTime { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public ChatListItemDesignModel()
        {
            this.Username = "PlayerCommander";
            this.Message = "You lose!";
            this.MessageTime = DateTime.MaxValue;
        }
    }
}