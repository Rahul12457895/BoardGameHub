namespace BoardGameHub
{
    // Class representing a human player in a game...........
    public class HumanPlayer : IPlayer
    {
        // Readonly field to store the position parser used by the player............
        private readonly IPositionParser positionParser;

        public HumanPlayer(IPositionParser positionParser)
        {
            ArgumentNullException.ThrowIfNull(positionParser);

            this.positionParser = positionParser;
        }

        // Method to create a move for the human player..........
        public IPosition CreateMove(IGame game, string input)
        {
            // Loop until a valid move is entered
            do
            {
                try
                {
                    // Attempt to parse the input and return the corresponding position
                    return ParsePosition(game, input);
                }
                catch (Invalid_UserInputException ex)
                {
                    Console.WriteLine("Invalid move: {0}", ex.Message);
                }
            } while (string.IsNullOrEmpty(input));
            throw new Invalid_UserInputException("");
        }

        // Private method to parse the position using the position parser............
        private IPosition ParsePosition(IGame game, string input)
        {
            return positionParser.ParsePosition(game, input);
        }

        // Method to get the player type (always returns "Human" for human players)............
        public string GetPlayerType()
        {
            return "Human";
        }
    }
}
