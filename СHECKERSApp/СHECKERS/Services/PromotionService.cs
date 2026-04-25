using CHECKERS.Models;
using CHECKERS.ViewModels;

namespace CHECKERS.Services
{
    public class PromotionService : IPromotionService
    {
        public void TryPromote(CellViewModel cell)
        {
            if (cell.IsWhite && cell.Row == 0)
                cell.Cellvalueenum = CellValueEnum.WhiteKing;
            else if (cell.IsBlack && cell.Row == 7)
                cell.Cellvalueenum = CellValueEnum.BlackKing;
        }
    }
}