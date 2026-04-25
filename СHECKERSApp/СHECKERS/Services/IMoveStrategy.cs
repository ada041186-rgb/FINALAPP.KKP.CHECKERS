using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IMoveStrategy
    {
        IEnumerable<Move> GetSimpleMoves(Board board, Cell cell);
        IEnumerable<Move> GetCaptureMoves(Board board, Cell cell);
    }
}