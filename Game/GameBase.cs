using BoardGameHub.Games;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.Marshalling;

namespace BoardGameHub
{
    // This class representing the base functionality...............
    public abstract class GameBase<TBoard, TPosition> : IGame where TBoard : IBoard where TPosition : IPosition
    {
        protected TBoard Board;  // The game board

        protected int currentPlayer;  // The current player ID

        protected readonly Stack<Move> UndoStack;  // Stack to store undo moves

        protected readonly Stack<Move> RedoStack;  // Stack to store redo moves

        // initializing the game base...............
        public GameBase(TBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            Board = board;
            UndoStack = new Stack<Move>();
            RedoStack = new Stack<Move>();
        }

        // Abstract method to initialize the game.............
        public abstract void Initialize();

        // Abstract method to make a move in the game.............
        public abstract bool MakeMove(int playerId, IPosition position);

        // Abstract method to check if the game is over..............
        public abstract bool IsGameOver();

        // Abstract method to get the winner of the game..............
        public abstract int GetWinner();

        // Virtual method to get the current state of the game board...........
        public virtual string GetBoardState()
        {
            return Board.ToString() ?? string.Empty;
        }

        // Virtual method to get the empty cells on the board............
        public virtual int[] GetEmptyCells()
        {
            return Board.GetEmptyCells();
        }

        // Abstract method to get the maximum position on the board.................
        public abstract IPosition GetMaxPosition();

        // Abstract method to get the ID of the current player................
        public abstract int GetCurrentPlayer();

        // Abstract method to check if undo is possible...............
        public abstract bool CanUndo();

        // Abstract method to undo a move................
        public abstract bool UndoMove();

        // Abstract method to check if redo is possible.............
        public abstract bool CanRedo();

        // Abstract method to redo a move................
        public abstract bool RedoMove();

        // Virtual method to get the move format hint for the current game...............
        public virtual string GetMoveFormatHint()
        {
            IMoveFormatHintProvider moveFormatHintProvider;
            if (this is Treblecross)
            {
                moveFormatHintProvider = new TreblecrossMoveFormatHintProvider();
            }
            else
            {
                // If no matching provider is found, throw an exception
                throw new NotSupportedException($"Move format hint provider not available for game type: {GetType().Name}");
            }

            return moveFormatHintProvider.GetMoveFormatHint(this);
        }

        // Virtual method to save the current game state to a file
        public virtual bool SaveGame(string player1Type, string player2Type, string fileName)
        {
            if (string.IsNullOrEmpty(player1Type) || string.IsNullOrEmpty(player2Type))
            {
                Console.WriteLine($"Error saving game: Invalid Player Type");
                return false;
            }
            try
            {
                var gameState = new GameState<TBoard>(player1Type, player2Type, currentPlayer, Board);
                string jsonData = JsonConvert.SerializeObject(gameState);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                File.WriteAllText(filePath, jsonData);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
                return false;
            }
        }

        // Virtual method to load a game state from a file
        public virtual (bool, string, string) LoadGame(string fileName)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                if (!File.Exists(filePath))
                    return (false, string.Empty, string.Empty);

                string jsonData = File.ReadAllText(filePath);

                GameState<TBoard>? gameState = JsonConvert.DeserializeObject<GameState<TBoard>>(jsonData);

                if (gameState != null)
                {
                    currentPlayer = gameState.CurrentPlayer ?? 0;
                    if (gameState.Board is null)
                    {
                        return (false, string.Empty, string.Empty);
                    }
                    Board = gameState.Board;
                    return (true, gameState.Player1Type, gameState.Player2Type);
                }
                else
                {
                    return (false, string.Empty, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                return (false, string.Empty, string.Empty);
            }
        }
    }
}
