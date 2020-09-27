namespace ChatServer.Repository.Models
{
    using System;

    public class Message
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        public string MessageText { get; set; }

        public string MessageType { get; set; }
    }
}
