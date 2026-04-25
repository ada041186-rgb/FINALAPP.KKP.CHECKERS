using CHECKERS.Models;
using System.Linq;

namespace CHECKERS.Services
{
    public class PieceSelectedState : IGameState
    {
        public void HandleCellClick(GameContext ctx, Cell cell)
        {
            var move = ctx.AvailableMoves.FirstOrDefault(m => m.To == cell);
            if (move != null)
            {
                ExecuteMove(ctx, move);
                return;
            }

            if (cell.BelongsTo(ctx.CurrentPlayer))
            {
                ctx.ClearHighlights();
                ctx.TransitionTo(new IdleState());
                ctx.HandleClick(cell);
                return;
            }

            ctx.ClearHighlights();
            ctx.SelectedCell = null;
            ctx.TransitionTo(new IdleState());
        }

        private void ExecuteMove(GameContext ctx, Move move)
        {
            ctx.ClearHighlights();

            move.To.Cellvalueenum = move.From.Cellvalueenum;
            move.From.Cellvalueenum = CellValueEnum.Empty;

            if (move.Captured != null)
                move.Captured.Cellvalueenum = CellValueEnum.Empty;

            ctx.Rules.TryPromote(ctx.Board, move.To);

            if (move.Captured != null)
            {
                var furtherCaptures = ctx.Rules
                    .GetAvailableMoves(ctx.Board, move.To)
                    .Where(m => m.Captured != null)
                    .ToList();

                if (furtherCaptures.Any())
                {
                    ctx.SelectedCell = move.To;
                    ctx.AvailableMoves = furtherCaptures.ToList();
                    move.To.Act = true;
                    foreach (var m in furtherCaptures) m.To.IsHighlighted = true;
                    return;
                }
            }

            ctx.GameOver = ctx.Rules.IsGameOver(ctx.Board, ctx.CurrentPlayer);
            if (!ctx.GameOver)
            {
                ctx.CurrentPlayer = ctx.CurrentPlayer == CellValueEnum.WhiteChecker
                    ? CellValueEnum.BlackChecker
                    : CellValueEnum.WhiteChecker;
            }

            ctx.SelectedCell = null;
            ctx.TransitionTo(new IdleState());
        }
    }
}