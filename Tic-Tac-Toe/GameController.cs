using System;
namespace Tic_Tac_Toe
{
	public class GameController
	{
		private Board _board;

		public GameController(Board board)
		{
			_board = board;
		}

		public void GetInput()
		{
			Console.WriteLine("Enter row: ");
			string rowInput = Console.ReadLine();
            Console.WriteLine("Enter column: ");
            string columnInput = Console.ReadLine();
            if (rowInput == null || columnInput == null)
			{
				Console.WriteLine("Input cannot be empty.");
				GetInput();
				return;
			}

			int row = int.Parse(rowInput);
			int column = int.Parse(columnInput);
			_board.SetPosition(row, column, 1);
		}
	}
}

