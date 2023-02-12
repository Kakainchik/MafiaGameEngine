using System;

namespace WPFApplication.Model
{
    public class ChatMessage
    {
        public DateTime MessageTime { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }

        public ChatMessage(string username, string message) 
            : this(username, message, DateTime.Now)
        {
            
        }

        public ChatMessage(string username, string message, DateTime time)
        {
            MessageTime = time;
            Username = username;
            Message = message;
        }
    }
}