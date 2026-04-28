using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public class MoveHistoryService : IMoveHistoryService
    {
        private readonly List<Move> _history = new();

        public void Record(Move move) => _history.Add(move);

        public IReadOnlyList<Move> GetHistory() => _history.AsReadOnly();

        public void Clear() => _history.Clear();
    }
}