using System;
using System.Collections;
using System.Data.Common;

namespace Tic_Tac_Toe
{
	public class Opponent
	{
        private const int BOARD_SIZE = 3;
		private const int O = 2;
		private const int X = 1;
		private const int NULL = 0;

        private Board _board;

        private List<int[]> _winningPlays;
		private List<int[]> _priorityPlays;
		private List<int[]> _plays;

        private int[][] _boardArray;
		private int[][] _verticals;
        private int[][] _horizontals;
        private int[][] _diagonals;

        public Opponent(Board board)
		{
			_board = board;
		}

		public void MakeMove()
		{
            _winningPlays = new List<int[]>();
            _priorityPlays = new List<int[]>();
            _plays = new List<int[]>();
            _boardArray = _board.GetBoard();
			_verticals = new int[3][];
            _horizontals = new int[3][];
            _diagonals = new int[2][];

            if (_boardArray[1][1] == NULL)
			{
				_board.SetPosition(1, 1, O);
				return;
			}

			CheckPlays();

			AddPlays();

			Random rand = new Random();
            int index = 0;
            var position = RandomPosition();
            while (!ValidPosition(position))
            {
                position = RandomPosition();
            }

            if (_plays.Count != 0)
            {
                index = rand.Next(_plays.Count);
                position = _plays[index];
            }

            if (_priorityPlays.Count != 0)
			{
				index = rand.Next(_priorityPlays.Count);
                position = _priorityPlays[index];
            }

            if (_winningPlays.Count != 0)
            {
                position = _winningPlays[0];
            }

			_board.SetPosition(position[0], position[1], O);
		}

		private void CheckPlays()
		{
            for (int column = 0; column < BOARD_SIZE; column++)
            {
                CheckVertical(column);
            }

            for (int row = 0; row < BOARD_SIZE; row++)
            {
                CheckHorizontal(row);
            }

            CheckDiagonal(0, 1);
            CheckDiagonal(2, -1);
        }

        private void CheckVertical(int column)
        {
            int[] columnArray = new int[4];
            columnArray[3] = 0;

            for (int row = 0; row < BOARD_SIZE; row++)
            {
                int value = _boardArray[row][column];
                columnArray[row] = value;
                if (value == X) columnArray[3]++;
            }

            _verticals[column] = columnArray;
        }

        private void CheckHorizontal(int row)
        {
            int[] rowArray = new int[4];
            rowArray[3] = 0;

            for (int column = 0; column < BOARD_SIZE; column++)
            {
                int value = _boardArray[row][column];
                rowArray[column] = value;
                if (value == X) rowArray[3]++;
            }

            _horizontals[row] = rowArray;
        }

        private void CheckDiagonal(int column, int direction)
        {
            int startColumn = column;
            int[] diagonalArray = new int[4];
            diagonalArray[3] = 0;

            for (int row = 0; row < BOARD_SIZE; row++)
            {
                int value = _boardArray[row][column];
                diagonalArray[row] = value;
                if (value == X) diagonalArray[3]++;
                column += 1 * direction;
            }

            int index = GenerateIndex(startColumn);
            _diagonals[index] = diagonalArray;
        }

        private void AddPlays()
        {
            VerticalPlay();
            HorizontalPlay();
            DiagonalPlay(0, 1);
            DiagonalPlay(2, -1);
        }

        private void VerticalPlay()
        {
            for (int column = 0; column < BOARD_SIZE; column++)
            {
                int[] columnArray = _verticals[column];
                int[] play = new int[2];

                int winnablePosition = Winnable(columnArray);
                if (winnablePosition != -1)
                {
                    play[0] = winnablePosition;
                    play[1] = column;
                    _winningPlays.Add(play);
                }

                if (columnArray[3] == 2 && columnArray.Contains(NULL))
                {
                    play[0] = Array.IndexOf(columnArray, NULL);
                    play[1] = column;
                    _priorityPlays.Add(play);
                    continue;
                }

                if (columnArray[3] == 1 && columnArray.Contains(NULL))
                {
                    play[0] = Array.IndexOf(columnArray, NULL);
                    play[1] = column;
                    _plays.Add(play);
                    continue;
                }
            }
        }

        private void HorizontalPlay()
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                int[] rowArray = _horizontals[row];
                int[] play = new int[2];

                int winnablePosition = Winnable(rowArray);
                if (winnablePosition != -1)
                {
                    play[0] = row;
                    play[1] = winnablePosition;
                    _winningPlays.Add(play);
                }

                if (rowArray[3] == 2 && rowArray.Contains(NULL))
                {
                    play[0] = row;
                    play[1] = Array.IndexOf(rowArray, NULL);
                    _priorityPlays.Add(play);
                    continue;
                }

                if (rowArray[3] == 1 && rowArray.Contains(NULL))
                {
                    play[0] = row;
                    play[1] = Array.IndexOf(rowArray, NULL);
                    _plays.Add(play);
                    continue;
                }
            }
        }

        private void DiagonalPlay(int startColumn, int direction)
        {
            int startIndex = GenerateIndex(startColumn);

            int[] diagonalArray = _diagonals[startIndex];
            int[] play = new int[2];
            int winnablePosition = Winnable(diagonalArray);
            if (winnablePosition != -1)
            {
                play[0] = winnablePosition;
                play[1] = startColumn + (direction * winnablePosition);
                _winningPlays.Add(play);
            }

            if (diagonalArray[3] == 2 && diagonalArray.Contains(NULL))
            {
                int index = Array.IndexOf(diagonalArray, NULL);
                play[0] = index;
                play[1] = startColumn + (direction * index);
                _priorityPlays.Add(play);
                return;
            }

            if (diagonalArray[3] == 1 && diagonalArray.Contains(NULL))
            {
                int index = Array.IndexOf(diagonalArray, NULL);
                play[0] = index;
                play[1] = startColumn + (direction * index);
                _plays.Add(play);
                return;
            }
        }

        private int GenerateIndex(int column)
        {
            int index = column - 1;
            if (index < 0) index = 0;
            return index;
        }

        private bool ValidPosition(int[] position)
        {
            if (_board.PositionTaken(position[0], position[1])) return false;
            return true;
        }

        private int[] RandomPosition()
        {
            Random random = new Random();
            int row = random.Next(BOARD_SIZE);
            int column = random.Next(BOARD_SIZE);
            int[] position = { row, column };
            return position;
        }

        private int Winnable(int[] array)
        {
            int oCount = 0;
            for (int value = 0; value < BOARD_SIZE; value++)
            {
                if (array[value] == O) oCount++;
            }
            if (oCount == 2) return Array.IndexOf(array, NULL);
            return -1;
        }
	}
}

