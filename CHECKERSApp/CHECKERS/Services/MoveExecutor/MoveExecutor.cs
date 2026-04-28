using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class MoveExecutor : IMoveExecutor
    {
        private readonly IPromotionService _promotion;
        private readonly IAfterMoveHandler _afterMove;
        private readonly IMoveHistoryService _history;
        private readonly IGameStatisticsService _statistics;

        public MoveExecutor(IPromotionService promotion,
                            IAfterMoveHandler afterMove,
                            IMoveHistoryService history,
                            IGameStatisticsService statistics)
        {
            _promotion = promotion;
            _afterMove = afterMove;
            _history = history;
            _statistics = statistics;
        }

        public void Execute(IStateContext ctx, Move move)
        {
            _statistics.RecordMove(move);
            _history.Record(move);

            ApplyMove(move);

            bool wasChecker = !move.From.IsKing;
            _promotion.TryPromote(move.To);
            if (wasChecker && move.To.IsKing)
                _statistics.RecordPromotion(ctx.CurrentPlayer);

            _afterMove.Handle(ctx, move);
        }

        private static void ApplyMove(Move move)
        {
            move.To.Cellvalueenum = move.From.Cellvalueenum;
            move.From.Cellvalueenum = CellValueEnum.Empty;
            if (move.Captured != null)
                move.Captured.Cellvalueenum = CellValueEnum.Empty;
        }
    }
}