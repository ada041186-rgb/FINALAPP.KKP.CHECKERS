using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CHECKERS.Services
{
    public class GameRules : IGameRules
    {
        private readonly IMoveStrategyFactory _factory;

        public GameRules(IMoveStrategyFactory factory)
        {
            _factory = factory;
        }

        public IReadOnlyList<Move> GetAvailableMoves(Board board, CellViewModel cell)
        {
            var strategy = _factory.Get(cell);
            var captures = strategy.GetCaptureMoves(board, cell).ToList();
            return captures.Count > 0
                ? captures
                : strategy.GetSimpleMoves(board, cell).ToList();
        }

        public IReadOnlyList<Move> GetAllCaptures(Board board, CellValueEnum player)
        {
            return board.GetPiecesFor(player)
                .SelectMany(c => _factory.Get(c.ViewModel).GetCaptureMoves(board, c.ViewModel))
                .ToList();
        }

        public bool MustCapture(Board board, CellValueEnum player) =>
            GetAllCaptures(board, player).Count > 0;

        public bool IsGameOver(Board board, CellValueEnum currentPlayer)
        {
            if (!board.HasOpponentPieces(currentPlayer)) return true;

            var opponent = currentPlayer == CellValueEnum.WhiteChecker ? CellValueEnum.BlackChecker : CellValueEnum.WhiteChecker;

            return !board.GetPiecesFor(opponent)
                .Any(c => GetAvailableMoves(board, c.ViewModel).Count > 0);
        }
    }
}