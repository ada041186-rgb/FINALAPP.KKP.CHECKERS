using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IBoardContext
    {
        Board Board { get; }
        CellValueEnum CurrentPlayer { get; set; }
        bool GameOver { get; set; }
        IGameRules Rules { get; }
    }
}
