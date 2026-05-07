using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Helpers
{
    /// <summary>
    /// Форматує список ходів у читабельний текст.
    /// Усуває дублювання коду між MainWindowViewModel та MoveHistoryViewModel.
    /// </summary>
    public static class MoveFormatter
    {
        public static List<string> Format(IReadOnlyList<Move> moves)
        {
            var result = new List<string>();

            for (int i = 0; i < moves.Count; i++)
            {
                var m = moves[i];
                string side = i % 2 == 0 ? "Білі" : "Чорні";
                string capture = m.Captured != null ? " ×" : "";
                result.Add(
                    $"{i + 1}. {side}: ({m.From.Row},{m.From.Column})" +
                    $"→({m.To.Row},{m.To.Column}){capture}");
            }

            return result;
        }
    }
}