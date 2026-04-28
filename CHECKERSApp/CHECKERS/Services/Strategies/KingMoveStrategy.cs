using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public class KingMoveStrategy : IMoveStrategy
    {
        private static readonly int[][] Dirs =
            { new[] { -1, -1 }, new[] { -1, 1 }, new[] { 1, -1 }, new[] { 1, 1 } };

        public IEnumerable<Move> GetSimpleMoves(Board board, CellViewModel cell)
        {
            foreach (var d in Dirs)
            {
                int r = cell.Row + d[0], c = cell.Column + d[1];
                while (board.InBounds(r, c) && board[r, c].IsEmpty)
                {
                    yield return new Move(cell, board[r, c].ViewModel);
                    r += d[0]; c += d[1];
                }
            }
        }

        public IEnumerable<Move> GetCaptureMoves(Board board, CellViewModel cell)
        {
            foreach (var d in Dirs)
            {
                int r = cell.Row + d[0], c = cell.Column + d[1];
                CellViewModel? captured = null;

                while (board.InBounds(r, c))
                {
                    var cur = board[r, c].ViewModel;

                    if (captured == null)
                    {
                        bool isOpponent = cell.IsWhite ? cur.IsBlack : cur.IsWhite;
                        if (isOpponent) captured = cur;
                        else if (!cur.IsEmpty) break;
                    }
                    else
                    {
                        if (cur.IsEmpty)
                            yield return new Move(cell, cur, captured);
                        else
                            break;
                    }
                    r += d[0]; c += d[1];
                }
            }
        }
    }
}