using System;

namespace RPScygni.Models
{
    public class Request
    {
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public bool Successful { get; set; }
        public Player Player { get; set; }
        public string PlayerName { get; set; }
        public Move NextMove { get; set; }
    }
}
