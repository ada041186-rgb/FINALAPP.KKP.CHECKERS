using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IMoveHistoryService
    {
        void Record(Move move);
        IReadOnlyList<Move> GetHistory();
        void Clear();
    }
}