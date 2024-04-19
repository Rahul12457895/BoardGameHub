namespace BoardGameHub
{
    // Abstract class representing a computer player in a game...........
    public abstract class ComputerPlayer : IPlayer
    {
        // Abstract method to create a move for the computer player...........
        public abstract IPosition CreateMove(IGame game, string input);

        // Method to get the player type (always returns "Computer" for computer players)...........
        public string GetPlayerType() => "Computer";
    }
}
