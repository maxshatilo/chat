namespace ChatServer.Domain.ViewModels
{
    using System;

    public class MessageViewModel
    {
        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        public string MessageText { get; set; }

        public string MessageType { get; set; }
    }
}
