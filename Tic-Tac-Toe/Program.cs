using Tic_Tac_Toe;

Board board = new Board();
GameController gameController = new GameController(board);
Opponent opponent = new Opponent(board);

while (true)
{
    board.Print();
    gameController.GetInput();
    if (board.DetermineComplete()) break;
    opponent.MakeMove();
    if (board.DetermineComplete()) break;
}

board.Print();