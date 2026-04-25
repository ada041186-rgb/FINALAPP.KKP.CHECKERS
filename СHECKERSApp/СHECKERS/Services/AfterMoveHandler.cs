using CHECKERS.Models;
using System.Linq;

namespace CHECKERS.Services
{
    public class AfterMoveHandler : IAfterMoveHandler
    {
        private readonly ITurnSwitcher _turnSwitcher;

        public AfterMoveHandler(ITurnSwitcher turnSwitcher)
        {
            _turnSwitcher = turnSwitcher;
        }

        public void Handle(IStateContext ctx, Move move)
        {
            if (TryChainCapture(ctx, move)) return;

            ctx.GameOver = ctx.Rules.IsGameOver(ctx.Board, ctx.CurrentPlayer);

            if (!ctx.GameOver)
                _turnSwitcher.Switch(ctx);

            ctx.SelectedCell = null;
            ctx.TransitionTo(new IdleState());
        }

        private static bool TryChainCapture(IStateContext ctx, Move move)
        {
            if (move.Captured == null) return false;

            var further = ctx.Rules
                .GetAvailableMoves(ctx.Board, move.To)
                .Where(m => m.Captured != null)
                .ToList();

            if (!further.Any()) return false;

            ctx.SelectedCell = move.To;
            ctx.AvailableMoves = further;
            move.To.Act = true;
            foreach (var m in further)
                m.To.IsHighlighted = true;

            return true;
        }
    }
}