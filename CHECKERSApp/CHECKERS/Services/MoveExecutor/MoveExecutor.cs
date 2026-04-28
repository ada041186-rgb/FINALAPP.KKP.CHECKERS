using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class MoveExecutor : IMoveExecutor
    {
        private readonly IPromotionService _promotion;
        private readonly IAfterMoveHandler _afterMove;

        public MoveExecutor(IPromotionService promotion, IAfterMoveHandler afterMove)
        {
            _promotion = promotion;
            _afterMove = afterMove;
        }

        public void Execute(IStateContext ctx, Move move)
        {
            ApplyMove(move);
            _promotion.TryPromote(move.To);
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