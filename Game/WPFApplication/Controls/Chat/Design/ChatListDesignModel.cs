using WPFApplication.Model;
using System;
using System.Collections.ObjectModel;

namespace WPFApplication.Controls.Chat.Design
{
    internal class ChatListDesignModel
    {
        public ObservableCollection<ChatMessage> Messages { get; set; }

        public ChatListDesignModel()
        {
            this.Messages = new ObservableCollection<ChatMessage>()
            {
                new ChatMessage(
                "PlayerCommander",
                "You win!",
                DateTime.MinValue),
                new ChatMessage(
                "PlayerCommander",
                "You lose!",
                DateTime.MinValue),
                new ChatMessage(
                "PCMode",
                "Probably!",
                DateTime.MinValue),
                new ChatMessage(
                "LongMess",
                "This is the longest message what I sent you ever don't mind?",
                DateTime.MaxValue)
            };
        }
    }
}