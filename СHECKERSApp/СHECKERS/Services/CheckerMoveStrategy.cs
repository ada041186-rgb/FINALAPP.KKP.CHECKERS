using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public class CheckerMoveStrategy : IMoveStrategy
    {
        public IEnumerable<Move> GetSimpleMoves(Board board, Cell cell)
        {
            int dir = cell.IsWhite ? -1 : 1;
            int[][] steps = { new[] { dir, -1 }, new[] { dir, 1 } };

            foreach (var s in steps)
            {
                int r = cell.Row + s[0], c = cell.Column + s[1];
                if (board.InBounds(r, c) && board[r, c].IsEmpty)
                    yield return new Move(cell, board[r, c]);
            }
        }

        public IEnumerable<Move> GetCaptureMoves(Board board, Cell cell)
        {
            int[][] dirs = { new[] { -1, -1 }, new[] { -1, 1 }, new[] { 1, -1 }, new[] { 1, 1 } };

            foreach (var d in dirs)
            {
                int mr = cell.Row + d[0], mc = cell.Column + d[1];
                int tr = cell.Row + 2 * d[0], tc = cell.Column + 2 * d[1];

                if (!board.InBounds(mr, mc) || !board.InBounds(tr, tc)) continue;

                var mid = board[mr, mc];
                var dest = board[tr, tc];

                bool midIsOpponent = cell.IsWhite ? mid.IsBlack : mid.IsWhite;

                if (midIsOpponent && dest.IsEmpty)
                    yield return new Move(cell, dest, mid);
            }
        }
    }
}