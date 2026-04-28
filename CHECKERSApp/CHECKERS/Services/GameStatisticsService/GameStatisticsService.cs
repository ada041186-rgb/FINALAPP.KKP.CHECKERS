using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class GameStatisticsService : IGameStatisticsService
    {
        public GameStatistics Current { get; private set; } = new();

        public void RecordMove(Move move)
        {
            Current.TotalMoves++;
            if (move.Captured == null) return;

            bool capturedIsWhite = move.Captured.IsWhite;
            if (capturedIsWhite) Current.WhiteCaptured++;
            else Current.BlackCaptured++;
        }

        public void RecordPromotion(CellValueEnum promotedPlayer)
        {
            if (promotedPlayer == CellValueEnum.WhiteChecker) Current.WhitePromotions++;
            else Current.BlackPromotions++;
        }

        public void Reset() => Current = new GameStatistics();
    }
}