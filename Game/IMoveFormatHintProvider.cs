namespace BoardGameHub
{
    // Represents the interface for providing move format hints.............
    public interface IMoveFormatHintProvider
    {
        // Retrieves the move format hint for the specified game.............
        string GetMoveFormatHint(IGame game);
    }

    // Provides hints format for move in the game............
    public class TreblecrossMoveFormatHintProvider : IMoveFormatHintProvider
    {
        // Retrieves the move format hint for the Treblecross game...............
        public string GetMoveFormatHint(IGame game)
        {
            TreblecrossPosition maxPosition = (TreblecrossPosition)game.GetMaxPosition();
            return $"position number (0-{maxPosition.Position})";
        }
    }
}
