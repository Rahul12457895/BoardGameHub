﻿namespace BoardGameHub
{
    // Class representing a position in the Treblecross game...............
    public class TreblecrossPosition : IPosition
    {
        // Property representing the position on the board................
        public int Position { get; set; }

        // Constructor to initialize the position.............
        public TreblecrossPosition(int position)
        {
            ArgumentNullException.ThrowIfNull(position); // Check if position is null (not applicable).............
            Position = position; // Set the position............
        }
    }
}
