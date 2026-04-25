using CHECKERS.ViewModels;

namespace CHECKERS.Services
{
    public interface IPromotionService
    {
        void TryPromote(CellViewModel cell);
    }
}