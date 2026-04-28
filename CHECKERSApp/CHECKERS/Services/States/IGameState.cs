using CHECKERS.ViewModels;

namespace CHECKERS.Services
{
    public interface IGameState
    {
        void HandleCellClick(IStateContext ctx, CellViewModel cell);
    }
}