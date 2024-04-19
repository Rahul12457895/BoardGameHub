using System;
using System.Text;

namespace BoardGameHub
{
    // Class representing the game board for Treblecross..............
    public class TreblecrossBoard : IBoard
    {
        private char[] board;

        // Property to access the board array.............
        public char[] Board
        {
            get => board;
            set => board = value;
        }

        // Property to get the size of the board...........
        public int BoardSize => board.Length;

  
        public TreblecrossBoard(int boardSize)
        {
            if (boardSize < 3)
            {
                throw new InvalidOperationException("BoardSize should be at least 3.");
            }

            board = new char[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                board[i] = ' ';
            }
        }

        // Fill a cell with a symbol..........
        public void FillCell(int position, char symbol)
        {
            if (position < 0 || position >= BoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
            }

            if (board[position] != ' ')
            {
                throw new InvalidOperationException("The cell is already filled.");
            }

            board[position] = symbol;
        }
        
        // Reset a cell to empty.............
        public void ResetCell(int position)
        {
            if (position < 0 || position >= BoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Invalid Position");
            }

            board[position] = ' ';
        }

        // Check if a cell is empty...............
        public bool IsCellEmpty(int position)
        {
            if (position < 0 || position >= BoardSize)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
            }

            return board[position] == ' ';
        }

        // Override ToString to represent the board as a string........
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

           
            sb.AppendLine(new string('-', BoardSize * 4));  

            
            for (int i = 0; i < BoardSize - 1; i++)  
            {
                sb.AppendFormat(" {0} |", board[i]);
            }

            sb.AppendFormat(" {0} ", board[BoardSize - 1]);

            sb.AppendLine();  

            
            sb.AppendLine(new string('-', BoardSize * 4)); 

            return sb.ToString();
        }

        // Get array of indices of empty cells..............
        public int[] GetEmptyCells()
        {
            List<int> emptyCells = new List<int>();
            for (int i = 0; i < BoardSize; i++)
            {
                if (board[i] == ' ')
                {
                    emptyCells.Add(i);
                }
            }
            return emptyCells.ToArray();
        }
    }
}
