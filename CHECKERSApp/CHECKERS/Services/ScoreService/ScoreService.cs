using CHECKERS.Helpers;
using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class ScoreService : IScoreService
    {
        public int WhiteWins { get; private set; }
        public int BlackWins { get; private set; }

        public void RecordWin(CellValueEnum winner)
        {
            if (winner.IsWhite()) WhiteWins++;
            else if (winner.IsBlack()) BlackWins++;
        }

        public void Reset()
        {
            WhiteWins = 0;
            BlackWins = 0;
        }
    }
}