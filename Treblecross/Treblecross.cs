using Newtonsoft.Json;

namespace BoardGameHub.Games
{
    // Class representing the Treblecross game...........
    public class Treblecross : GameBase<TreblecrossBoard, TreblecrossPosition>
    {
        // Game rules interface............
        public IGameRules GameRules;

        public Treblecross(TreblecrossBoard board, IGameRules gameRules) : base(board)
        {
            ArgumentNullException.ThrowIfNull(gameRules);
            GameRules = gameRules;
        }

        // Initialize the game.........
        public override void Initialize()
        {
            currentPlayer = 1;
            UndoStack.Clear();
            RedoStack.Clear();
        }

        // Make a move in the game...........
        public override bool MakeMove(int playerId, IPosition position)
        {
            var tPosition = ((TreblecrossPosition)position).Position;
            if (!IsValidPlayerId(playerId))
            {
                throw new ArgumentException("Invalid player ID.");
            }

            if (!IsValidPosition(tPosition))
            {
                throw new ArgumentException("Invalid position.");
            }

            if (!GameRules.IsValidMove(Board, position))
            {
                return false;
            }

            char symbol = (playerId == 1) ? 'X' : 'O';
            Board.FillCell(tPosition, symbol);
            UndoStack.Push(new Move(playerId, new TreblecrossPosition(tPosition)));
            RedoStack.Clear();
            SwitchTurns();
            return true;
        }

        // Switch turns between players...........
        private void SwitchTurns()
        {
            currentPlayer = (currentPlayer == 1) ? 2 : 1;
        }

        // Check if the game is over............
        public override bool IsGameOver()
        {
            return GameRules.IsGameOver(Board);
        }

        // Get the winner of the game
        public override int GetWinner()
        {
            return GameRules.GetWinner(Board);
        }

        // Get the maximum position on the board..............
        public override TreblecrossPosition GetMaxPosition()
        {
            return new TreblecrossPosition(Board.BoardSize - 1);
        }
        
        // Get the current player...........
        public override int GetCurrentPlayer()
        {
            return currentPlayer;
        }
        
        // Check if undo is possible................
        public override bool CanUndo()
        {
            return UndoStack.Count > 0;
        }

        // Undo the last move
        public override bool UndoMove()
        {
            if (CanUndo())
            {
                var move = UndoStack.Pop();
                var position = ((TreblecrossPosition)move.Position).Position;
                Board.ResetCell(position);
                RedoStack.Push(move);
                SwitchTurns();
                return true;
            }
            return false;
        }

        // Check if redo is possible.............
        public override bool CanRedo()
        {
            return RedoStack.Count > 0;
        }

        // Redo the last move..............
        public override bool RedoMove()
        {
            var move = RedoStack.Peek();
            int position = ((TreblecrossPosition)move.Position).Position;
            var symbol = (move.PlayerId == 1) ? 'X' : 'O';
            Board.FillCell(position, symbol);
            SwitchTurns();
            RedoStack.Pop();
            UndoStack.Push(move);
            return true;
        }

        // Check if the player ID is valid...............
        private bool IsValidPlayerId(int playerId)
        {
            return playerId == 1 || playerId == 2;
        }

        // Check if the position is valid..............
        private bool IsValidPosition(int position)
        {
            return position >= 0 && position < Board.BoardSize;
        }
    }
}
