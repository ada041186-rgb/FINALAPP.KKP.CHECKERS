using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IGameContext
    {
        CellValueEnum CurrentPlayer { get; }
        bool GameOver { get; }
        void HandleClick(CellViewModel cell);
        void NewGame();
        IReadOnlyList<CellViewModel> GetCellViewModels(); 
        CellValueEnum? GetWinner();
    }
}