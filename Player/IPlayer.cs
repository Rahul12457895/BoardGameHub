namespace BoardGameHub
{
    // Interface representing a player in a game.............
    public interface IPlayer
    {
        // Method to create a move based on the input provided by the player...............
        IPosition CreateMove(IGame game, string input);

        // Method to get player type --> "Human" or "Computer"........
        string GetPlayerType();
    }
}
