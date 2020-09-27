namespace ChatServer.Domain.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class AggregatedMessagesViewModel
    {
        public class MessagesByDate
        {
            public DateTime Date { get; set; }

            public List<MessagesByHour> MessagesByHour { get; set; }
        }

        public class MessagesByHour
        {
            public int Hour { get; set; }

            public List<HourStatistics> Messages { get; set; }
        }

        public class HourStatistics
        {
            public string Name { get; set; }

            public int Count { get; set; }

        }
    }
}
