using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class ScoreService : IScoreService
    {
        public int WhiteWins { get; private set; }
        public int BlackWins { get; private set; }

        public void RecordWin(CellValueEnum winner)
        {
            if (winner == CellValueEnum.WhiteChecker) WhiteWins++;
            else BlackWins++;
        }

        public void Reset()
        {
            WhiteWins = 0;
            BlackWins = 0;
        }
    }
}