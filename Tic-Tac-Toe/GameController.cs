namespace Tic_Tac_Toe
{
	/// <summary>
	/// Class <c>GameController</c> controls game input and determines when to end
	/// </summary>

	public class GameController
	{
		private Board _board;

		public GameController(Board board)
		{
			_board = board;
		}

		/// <summary>
		/// Method <c>GetInput</c> collects and parses user input
		/// </summary>
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

            if (_board.PositionTaken(row, column))
            {
                Console.WriteLine("Position is taken.");
                GetInput();
                return;
            }

            _board.SetPosition(row, column, 1);
		}
	}
}

