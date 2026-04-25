using CHECKERS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CHECKERS.Services
{
   
    public class GameRules
    {
        public IReadOnlyList<Move> GetAvailableMoves(Board board, Cell cell)
        {
            var strategy = MoveStrategyFactory.Get(cell);
            var captures = strategy.GetCaptureMoves(board, cell).ToList();
            return captures.Count > 0
                ? captures
                : strategy.GetSimpleMoves(board, cell).ToList();
        }

        public IReadOnlyList<Move> GetAllCaptures(Board board, CellValueEnum player)
        {
            return board
                .Where(c => c.BelongsTo(player))
                .SelectMany(c => MoveStrategyFactory.Get(c).GetCaptureMoves(board, c))
                .ToList();
        }

        public bool MustCapture(Board board, CellValueEnum player) =>
            GetAllCaptures(board, player).Count > 0;

        public bool IsGameOver(Board board, CellValueEnum currentPlayer) =>
            !board.HasOpponentPieces(currentPlayer);

        public void TryPromote(Board board, Cell cell)
        {
            if (cell.IsWhite && cell.Row == 0)
                cell.Cellvalueenum = CellValueEnum.WhiteKing;
            else if (cell.IsBlack && cell.Row == 7)
                cell.Cellvalueenum = CellValueEnum.BlackKing;
        }
    }
}