using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Linq;

namespace CHECKERS.Services
{
    public class IdleState : IGameState
    {
        public void HandleCellClick(IStateContext ctx, CellViewModel cell)
        {
            if (!cell.BelongsTo(ctx.CurrentPlayer)) return;

            var moves = ctx.Rules.GetAvailableMoves(ctx.Board, cell);

            if (ctx.Rules.MustCapture(ctx.Board, ctx.CurrentPlayer))
            {
                var captures = ctx.Rules.GetAllCaptures(ctx.Board, ctx.CurrentPlayer);
                if (!captures.Any(m => m.From == cell)) return;
                moves = captures.Where(m => m.From == cell).ToList();
            }

            if (!moves.Any()) return;

            ctx.ClearHighlights();
            ctx.SelectedCell = cell;
            ctx.AvailableMoves = moves.ToList();
            cell.Act = true;

            foreach (var m in moves)
                m.To.IsHighlighted = true;

            ctx.TransitionTo(new PieceSelectedState());
        }
    }
}