using BoardGameHub.Games;

namespace BoardGameHub
{
    public class Program
    {
        // Main method
        static void Main(string[] args)
        {
            // Welcome message..............
            Console.WriteLine("Welcome to the Board Game Hub!");

            IGame game;

            // Choices for starting or loading a game.............
            Console.WriteLine("1. Start a new Treblecross game");
            Console.WriteLine("2. Load Treblecross game");
                      
            string? gameInput = Console.ReadLine();

            // Switch statement to handle user choice............
            switch (gameInput)
            {
                case "1":
                    // Start a new game............
                    Console.WriteLine("Enter the board size:");
                    string? boardSizeInput = Console.ReadLine();
                    if (!int.TryParse(boardSizeInput, out int boardSize) || boardSize < 3)
                    {
                        // Invalid board size............
                        Console.WriteLine("Invalid board size. Exiting...");
                        return;
                    }
                    game = new Treblecross(new TreblecrossBoard(boardSize), new TreblecrossGameRules());
                    InitializeNewGame(game);
                    DeclareWinner(game);
                    Console.WriteLine("Game over!");
                    break;
                case "2":
                    // Load a saved game...............
                    game = new Treblecross(new TreblecrossBoard(3), new TreblecrossGameRules());
                    LoadSavedGame(game);
                    DeclareWinner(game);
                    Console.WriteLine("Game over!");
                    break;
                default:
                    // Invalid option............
                    Console.WriteLine("Invalid Option");
                    return;
            }

        }


        // Initialize a new game.................
        private static void InitializeNewGame(IGame game)
        {
            // Set up players..............
            IPositionParser positionParser = new TreblecrossPositionParser();
            game.Initialize();
            IPlayer player1 = new HumanPlayer(positionParser);
            IPlayer player2;

            try
            {
                // Choose opponent type................
                string opponentChoice;
                bool isComputerOpponent;

                do
                {
                    Console.WriteLine("Select opponent:");
                    Console.WriteLine("1. Human");
                    Console.WriteLine("2. Computer");

                    opponentChoice = Console.ReadLine() ?? string.Empty;
                } while (opponentChoice != "1" && opponentChoice != "2");

                isComputerOpponent = opponentChoice == "2";
                player2 = isComputerOpponent ? new TreblecrossAI() : new HumanPlayer(positionParser);
            }
            catch (Exception ex)
            {
                // Error during game setup..............
                Console.Error.WriteLine("Error during game setup: {0}", ex.Message);
                return;
            }

            // Start the game..............
            PlayGame(game, player1, player2);

        }

        // Load a saved game............
        private static void LoadSavedGame(IGame game)
        {
            var (loaded, player1Type, player2Type) = game.LoadGame("saved_game.json");
            if (!loaded)
            {
                // Failed to load game............
                Console.WriteLine("Failed to load game. Starting new game with default board size.");
                InitializeNewGame(game);
            }
            else
            {
                // Game loaded successfully..............
                Console.WriteLine("Game loaded successfully");
                DisplayBoard(game);
                IPositionParser positionParser = new TreblecrossPositionParser();
                IPlayer player1 = new HumanPlayer(positionParser);
                IPlayer player2 = player2Type == "Human" ? new HumanPlayer(positionParser) : new TreblecrossAI();
                PlayGame(game, player1, player2);
            }
        }


        // Main game loop..............
        private static void PlayGame(IGame game, IPlayer player1, IPlayer player2)
        {
            // Display features for the player..............
            Console.WriteLine("Options are 'undo', 'redo', 'save', or 'exit'");

            while (!game.IsGameOver())
            {
                // Determine current player.............
                int playerId = game.GetCurrentPlayer();
                IPlayer currentPlayer = (playerId == 1) ? player1 : player2;

                string? input;
                try
                {
                    // Handle player moves............
                    if (currentPlayer.GetPlayerType() == "Human")
                    {
                        Console.WriteLine("[Player {0}] Enter your move (format: {1}):", playerId, game.GetMoveFormatHint());

                        input = Console.ReadLine() ?? string.Empty;

                        // Handle special commands...............
                        if (input.Equals("undo", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (game.CanUndo())
                            {
                                game.UndoMove();
                                playerId = game.GetCurrentPlayer();
                                if (player2.GetPlayerType() == "Computer" && playerId == 2)
                                {
                                    Console.WriteLine("Computer Move undone");
                                    game.UndoMove();
                                    Console.WriteLine("[Player 1] Move undone", playerId);
                                }
                                else
                                {
                                    Console.WriteLine("[Player {0}] Move undone", playerId);
                                }
                                DisplayBoard(game);
                            }
                            else
                            {
                                Console.WriteLine("No moves to undo.");
                            }
                            continue;
                        }
                        else if (input.Equals("redo", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (game.CanRedo())
                            {
                                game.RedoMove();
                                playerId = game.GetCurrentPlayer();
                                if (player2.GetPlayerType() == "Computer" && playerId == 2)
                                {
                                    Console.WriteLine("[Player 1] Move redone", playerId);
                                    game.RedoMove();
                                    Console.WriteLine("Computer Move redone");
                                }
                                else
                                {
                                    Console.WriteLine("[Player {0}] Move redone", playerId);
                                }
                                DisplayBoard(game);
                            }
                            else
                            {
                                Console.WriteLine("No moves to redo.");
                            }
                            continue;
                        }
                        else if (input.Equals("save", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (game.SaveGame(player1.GetPlayerType(), player2.GetPlayerType(), "saved_game.json"))
                            {
                                Console.WriteLine("Game Saved.");
                            }
                            continue;
                        }
                        else if (input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                        {
                            break;
                        }
                        IPosition move = currentPlayer.CreateMove(game, input);

                        if (game.MakeMove(playerId, move))
                        {
                            DisplayBoard(game);
                        }
                        else
                        {
                            Console.WriteLine("Invalid move. Please try again.");
                        }
                    }
                    else
                    {
                        // Handle computer player.............
                        IPosition move = currentPlayer.CreateMove(game, "");
                        if (game.MakeMove(playerId, move))
                        {
                            Console.WriteLine("Computer Played: {0}", ((TreblecrossPosition)move).Position);
                            DisplayBoard(game);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Error during player move.............
                    Console.Error.WriteLine("Error during player move: {0}", ex.Message);
                }
            }

        }

        // Display the game board..............
        private static void DisplayBoard(IGame game)
        {
            Console.WriteLine(game.GetBoardState());
        }


        // Declare the winner...........
        private static void DeclareWinner(IGame game)
        {
            int winner = game.GetWinner();
            if (winner > 0)
            {
                Console.WriteLine("Player " + winner + " wins!");
            }
            else
            {
                Console.WriteLine("Stalemate! No winner.");
            }
        }
    }
}
