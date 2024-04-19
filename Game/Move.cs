namespace BoardGameHub
{
    // Represents a move made by a player in a game
    public record Move
    {
        public int PlayerId { get; }

        public IPosition Position { get; }
        public Move(int playerId, IPosition position)
        {
            PlayerId = playerId;
            Position = position;
        }
    }
}
