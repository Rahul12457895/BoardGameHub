using BoardGameHub.Games;

namespace BoardGameHub
{
    // Interface for parsing user input into game positions...........
    public interface IPositionParser
    {
        // Parses user input into a game position.............
        IPosition ParsePosition(IGame game, string userInput);
    }

    // Implementation of IPositionParser for Treblecross game..............
    public class TreblecrossPositionParser : IPositionParser
    {
        // Parses user input into a TreblecrossPosition.................
        public IPosition ParsePosition(IGame game, string userInput)
        {
            ArgumentNullException.ThrowIfNull(game);
            ArgumentNullException.ThrowIfNull(userInput);
            if (int.TryParse(userInput, out int position))
            {
                int maxPosition = ((TreblecrossPosition)game.GetMaxPosition()).Position;
                if (position < 0 || position > maxPosition)
                {
                    throw new Invalid_UserInputException($"Please enter a number between 0 and {maxPosition}");
                }

                return new TreblecrossPosition(position);
            }

            // Throw exception for invalid input.............
            throw new Invalid_UserInputException("Invalid input, make a valid move");
        }
    }
}
