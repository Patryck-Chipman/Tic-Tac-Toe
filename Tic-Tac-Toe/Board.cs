namespace Tic_Tac_Toe
{
	/// <summary>
	/// Class <c>Board</c> controls all aspects of the board (ie rendering and positions)
	/// </summary>
	public class Board
	{
		private const int BOARD_SIZE = 3;
		private const int NULL = 0;
		private const int X = 1;
		private const int O = 2;

		private int[][] _board;

		public Board()
		{
            _board = new int[BOARD_SIZE][];

            for (int i = 0; i < BOARD_SIZE; i++)
			{
				_board[i] = new int[BOARD_SIZE];
				for (int j = 0; j < BOARD_SIZE; j++)
				{
					_board[i][j] = NULL;
				}
			}
		}

		/// <summary>
		/// Method <c>Print</c> renders the current board in ASCII text
		/// </summary>
		public void Print()
		{
			foreach (var array in _board)
			{
                for (int i = 0; i < BOARD_SIZE; i++)
                {
					int indexValue = array[i];
					char value;
					if (indexValue == O) value = 'O';
					else if (indexValue == X) value = 'X';
					else value = '.';
                    Console.Write(value + "|");
                }
                Console.WriteLine("\n-|-|-");
            }
		}

		/// <summary>
		/// Method <c>SetPosition</c> sets the position on the board to a value (ie X or O)
		/// </summary>
		/// <param name="row"></param> the row on the table
		/// <param name="column"></param> the column on the table
		/// <param name="value"></param> the value to put (X = 1, O = 2)
		public void SetPosition(int row, int column, int value)
		{
			_board[row][column] = value;
		}

		/// <summary>
		/// Method <c>DetermineComplete</c> checks to see if a side has won or if there's a cats game
		/// </summary>
		/// <returns>true</returns> if the game is over
		public bool DetermineComplete()
		{
			// Check all rows and columns ro see if the game has been won
            for (int i = 0; i < BOARD_SIZE; i++)
            {
				for (int j = 0; j < BOARD_SIZE; j++)
				{
                    if (WinHorizontal(i, j, X)) return true;
                    if (WinHorizontal(i, j, O)) return true;

                    if (WinVertical(i, j, X)) return true;
                    if (WinVertical(i, j, O)) return true;

                    if (WinDiagonal(i, j, X)) return true;
                    if (WinDiagonal(i, j, O)) return true;
                }
            }

			foreach (var array in _board)
			{
				foreach (var value in array)
				{
					// Game board is not empty
					if (value == 0) return false;
				}
			}

			Console.WriteLine("Cats game!");

			return true;
		}

		/// <summary>
		/// Method <c>GetBoard</c> returns the current game board
		/// </summary>
		/// <returns>current game board</returns>
		public int[][] GetBoard()
		{
			return _board;
		}

		/// <summary>
		/// Method <c>PositionTaken</c> checks if the given position is occupied
		/// </summary>
		/// <param name="row"></param> row to check
		/// <param name="column"></param> column to check
		/// <returns>true</returns> if the position is taken
		public bool PositionTaken(int row, int column)
		{
			if (_board[row][column] == 0) return false;
			return true;
		}

		private bool WinHorizontal(int row, int column, int turn)
		{
			if (column != 0) return false;
			for (int i = column; i < BOARD_SIZE; i++)
			{
				if (_board[row][i] != turn) return false;
			}

            DisplayWinner(turn);

            return true;
		}

		private bool WinVertical(int row, int column, int turn)
		{
			if (row != 0) return false;
			for (int i = row; i < BOARD_SIZE; i++)
			{
				if (_board[i][column] != turn) return false;
			}

            DisplayWinner(turn);

            return true;
		}

		private bool WinDiagonal(int row, int column, int turn)
		{
            if (row != 0) return false;
			if (column == turn) return false;
			if (_board[row][column] != turn) return false;
			if (_board[1][1] != turn) return false;
            int finalColumn = int.Abs(column - 2);
			if (_board[BOARD_SIZE - 1][finalColumn] != turn) return false;

			DisplayWinner(turn);

			return true;
		}

		private void DisplayWinner(int turn)
		{
            string winner;
            if (turn == X) winner = "X";
            else winner = "O";
            Console.WriteLine(String.Format("{0} wins!", winner));
        }
	}
}

