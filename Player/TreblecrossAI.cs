using BoardGameHub.Games;
using System;

namespace BoardGameHub
{
    // Class representing an AI player for the Treblecross game
    public class TreblecrossAI : ComputerPlayer
    {
        // Method to create a move for the "AI player"...............
        public override IPosition CreateMove(IGame game, string input)
        {
            // Analyze game state (board layout, opponent moves).............
            if (game is not Treblecross treblecrossGame)
            {
                throw new ArgumentException("Invalid game type. Expected Treblecross.");
            }

            int[] emptyCells = treblecrossGame.GetEmptyCells();

            // choose a random available move...............
            Random random = new();
            int chosenMove = emptyCells[random.Next(emptyCells.Length)];

            // Return the chosen move as a TreblecrossPosition...........
            return new TreblecrossPosition(chosenMove);
        }
    }
}
