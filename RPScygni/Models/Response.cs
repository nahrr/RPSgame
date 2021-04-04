namespace RPScygni.Models
{
    public class Response
    {
        public bool Successful { get; set; }
        public GameStatus Status { get; set; }
        public GameIdResponse GameId { get; set; }
        public Error Error { get; set; }
        public JoinGameResponse JoinedGame { get; set; }
        public Game Name { get; set; }
        public PlayGameResponse PlayedGame { get; set; }
    }
}
