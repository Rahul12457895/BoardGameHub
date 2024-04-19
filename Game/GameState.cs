using Newtonsoft.Json;

namespace BoardGameHub
{
    // Represents the state of a game...............
    public record GameState<TBoard> where TBoard : IBoard
    {
        // Player Type --> "Human" or "Computer"........... 
        public string Player1Type { get; init; }

        // Player Type --> "Human" or "Computer"........... 
        public string Player2Type { get; init; }

        // ID of the current player............
        public int? CurrentPlayer { get; init; }

        // The game board state
        public TBoard? Board { get; init; }

        // Constructor to initialize the game state................
        public GameState(string player1Type, string player2Type, int? currentPlayer = 1, TBoard? board = default)
        {
            Player1Type = player1Type;
            Player2Type = player2Type;
            CurrentPlayer = currentPlayer;
            Board = board;
        }
    }
}
