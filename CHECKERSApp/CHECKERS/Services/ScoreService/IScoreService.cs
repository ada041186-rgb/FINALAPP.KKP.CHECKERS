using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IScoreService
    {
        int WhiteWins { get; }
        int BlackWins { get; }
        void RecordWin(CellValueEnum winner);
        void Reset();
    }
}