using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IStateContext
    {
        Board Board { get; }
        CellValueEnum CurrentPlayer { get; set; }
        bool GameOver { get; set; }
        GameRules Rules { get; }
        IMoveExecutor MoveExecutor { get; }
        List<Move> AvailableMoves { get; set; }
        CellViewModel? SelectedCell { get; set; }
        void TransitionTo(IGameState state);
        void ClearHighlights();
        void HandleClick(CellViewModel cell);
    }
}