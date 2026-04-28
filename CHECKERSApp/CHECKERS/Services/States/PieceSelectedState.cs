using CHECKERS.ViewModels;
using System.Linq;

namespace CHECKERS.Services
{
    public class PieceSelectedState : IGameState
    {
        public void HandleCellClick(IStateContext ctx, CellViewModel cell)
        {
            var move = ctx.AvailableMoves.FirstOrDefault(m => m.To == cell);

            if (move != null)
            {
                ctx.ClearHighlights();
                ctx.MoveExecutor.Execute(ctx, move);
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
    }
}