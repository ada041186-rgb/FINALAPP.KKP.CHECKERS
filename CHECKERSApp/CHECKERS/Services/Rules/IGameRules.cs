using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IGameRules
    {
        IReadOnlyList<Move> GetAvailableMoves(Board board, CellViewModel cell);
        IReadOnlyList<Move> GetAllCaptures(Board board, CellValueEnum player);
        bool MustCapture(Board board, CellValueEnum player);
        bool IsGameOver(Board board, CellValueEnum currentPlayer);
    }
}