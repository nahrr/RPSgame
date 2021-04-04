using System;

namespace RPScygni.Models
{
    public class Game
    {
        public string Name { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public bool IsFinished { get; set; }
        public bool IsFull { get; set; }
        public GameStatus GameStatus { get; set; }
        public Guid GameId { get; set; }
    }
}
