using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IGameContext
    {
            Board Board { get; }
            CellValueEnum CurrentPlayer { get; set; }
            bool GameOver { get; }
            void HandleClick(CellViewModel cell);
            void NewGame();
            void SwitchTurn();                       
            IReadOnlyList<CellViewModel> GetCellViewModels();
            CellValueEnum? GetWinner();
            void ClearHighlights();
        }
    }