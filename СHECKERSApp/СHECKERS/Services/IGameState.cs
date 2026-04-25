using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IGameState
    {
        void HandleCellClick(GameContext ctx, Cell cell);
    }
}