using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class BoardSetupService : IBoardSetupService
    {
        public void Setup(Board board)
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 8; c++)
                    if ((r + c) % 2 != 0)
                        board[r, c].ViewModel.Cellvalueenum = CellValueEnum.BlackChecker;

            for (int r = 5; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if ((r + c) % 2 != 0)
                        board[r, c].ViewModel.Cellvalueenum = CellValueEnum.WhiteChecker;
        }
    }
}