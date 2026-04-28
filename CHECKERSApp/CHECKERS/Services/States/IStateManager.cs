using CHECKERS.Models;
using CHECKERS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHECKERS.Services.States
{
    public interface IStateManager
    {
        List<Move> AvailableMoves { get; set; }
        CellViewModel? SelectedCell { get; set; }
        void TransitionTo(IGameState state);
        void ClearHighlights();
        void HandleClick(CellViewModel cell);
    }
}
