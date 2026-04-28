using CHECKERS.ViewModels;

namespace CHECKERS.Services
{
    public interface IMoveStrategyFactory
    {
        IMoveStrategy Get(CellViewModel cell);
    }
}