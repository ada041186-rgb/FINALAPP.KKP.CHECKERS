using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IAIOpponentService
    {
        AIDifficulty Difficulty { get; set; }
        bool IsAITurn(CellValueEnum currentPlayer);
        Move? ChooseMove(Board board, CellValueEnum player);
    }
}