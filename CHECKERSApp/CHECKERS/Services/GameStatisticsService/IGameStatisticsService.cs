using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IGameStatisticsService
    {
        GameStatistics Current { get; }
        void RecordMove(Move move);
        void RecordPromotion(CellValueEnum promotedPlayer);
        void Reset();
    }
}