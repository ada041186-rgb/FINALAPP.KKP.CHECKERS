using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IMoveStrategy
    {
        IEnumerable<Move> GetSimpleMoves(Board board, CellViewModel cell);
        IEnumerable<Move> GetCaptureMoves(Board board, CellViewModel cell);
    }
}