using System;

namespace RPScygni.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Move Move { get; set; }
    }
}
