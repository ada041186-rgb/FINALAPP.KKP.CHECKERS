using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class GameSnapshotService : IGameSnapshotService
    {
        public GameSnapshot Build(IGameContext ctx, int totalMoves)
        {
            var snap = new GameSnapshot
            {
                CurrentPlayer = ctx.CurrentPlayer,
                TotalMoves = totalMoves
            };

            foreach (var cell in ctx.Board)
                snap.Cells.Add(new CellSnapshot
                {
                    Row = cell.Row,
                    Column = cell.Column,
                    Value = cell.Cellvalueenum
                });

            return snap;
        }

        public void Restore(IGameContext ctx, GameSnapshot snap)
        {
            ctx.NewGame();

            foreach (var cs in snap.Cells)
                ctx.Board[cs.Row, cs.Column].Cellvalueenum = cs.Value;

            ctx.CurrentPlayer = snap.CurrentPlayer;
        }
    }
}