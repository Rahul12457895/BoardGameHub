namespace BoardGameHub
{
    // Represents the interface for a game board.................
    public interface IBoard
    {
        // Array representing the board state..........
        char[] Board { get; }

        // Size of the board.........
        int BoardSize { get; }

        // Checks if a cell at a given position is empty..........
        bool IsCellEmpty(int position);

        // Fills a cell at a given position with a symbol.............
        void FillCell(int position, char symbol);

        // Resets the cell at a given position to empty..............
        void ResetCell(int position);

        // Retrieves an array of empty cell positions on the board..............
        int[] GetEmptyCells();
    }
}
