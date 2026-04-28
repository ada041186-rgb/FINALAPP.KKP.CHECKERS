using CHECKERS.Models;
using System.Linq;

namespace CHECKERS.Services
{
    public class HintService : IHintService
    {
        private readonly IGameRules _rules;

        public HintService(IGameRules rules)
        {
            _rules = rules;
        }

        public MoveHint? GetBestMove(Board board, CellValueEnum player)
        {
            var allMoves = board
                .Where(c => c.BelongsTo(player))
                .SelectMany(c => _rules.GetAvailableMoves(board, c.ViewModel)
                    .Select(m => new MoveHint(m, m.Captured != null ? 1 : 0)))
                .ToList();

            if (!allMoves.Any()) return null;

            return allMoves
                .OrderByDescending(h => h.CaptureCount)
                .First();
        }
    }
}