namespace BoardGameHub
{
    // Class implementing game rules for Treblecross..............
    public class TreblecrossGameRules : IGameRules
    {
        // Check if a move is valid................
        public bool IsValidMove(IBoard board, IPosition position)
        {
            if (position is TreblecrossPosition treblecrossPosition)
            {
                return treblecrossPosition.Position >= 0
                    && treblecrossPosition.Position < board.BoardSize
                    && board.IsCellEmpty(treblecrossPosition.Position);
            }
            return false;
        }

        // Check if the game is over............
        public bool IsGameOver(IBoard board)
        {
            // Check for horizontal wins..............
            for (int i = 0; i <= board.BoardSize - 3; i++)
            {
                if (board.Board[i] != ' ' &&
                    board.Board[i] == board.Board[i + 1] &&
                    board.Board[i + 1] == board.Board[i + 2])
                {
                    return true;
                }
            }

            // Check for no more empty spaces (stalemate)..................
            for (int i = 0; i < board.BoardSize; i++)
            {
                if (board.Board[i] == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        // Get the winner of the game...................
        public int GetWinner(IBoard board)
        {
            if (IsGameOver(board))
            {
                // Check for horizontal wins..............
                for (int i = 0; i <= board.BoardSize - 3; i++)
                {
                    if (board.Board[i] != ' ' &&
                        board.Board[i] == board.Board[i + 1] &&
                        board.Board[i + 1] == board.Board[i + 2])
                    {
                        return board.Board[i] == 'X' ? 1 : 2;
                    }
                }
            }

            return -1; // No winner yet...........
        }
    }
}
