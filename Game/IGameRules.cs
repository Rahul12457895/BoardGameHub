namespace BoardGameHub
{
    // Represents the interface for game rules................
    public interface IGameRules
    {
        // Checks if a move is valid on the board at the specified position.............
        bool IsValidMove(IBoard board, IPosition position);

        // Checks if the game is over based on the current state of the board...............
        bool IsGameOver(IBoard board);

        // Retrieves the winner of the game based on the current state of the board..............
        int GetWinner(IBoard board);
    }
}
