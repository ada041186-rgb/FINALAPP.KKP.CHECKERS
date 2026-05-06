using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class BoardSetupService : IBoardSetupService
    {
        public void Setup(Board board)
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < Board.Size; c++)
                    if ((r + c) % 2 != 0)
                        board[r, c].Cellvalueenum = CellValueEnum.BlackChecker;

            for (int r = Board.Size - 3; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    if ((r + c) % 2 != 0)
                        board[r, c].Cellvalueenum = CellValueEnum.WhiteChecker;
        }
    }
}