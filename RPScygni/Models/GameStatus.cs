namespace RPScygni.Models
{
    public enum GameStatus
    {
        Created = 1,
        PlayerOneMovePending = 2,
        PlayerTwoMovePending = 3,
        Tie = 4,
        PlayerOneWon = 5,
        PlayerTwoWon = 6
    };
}
