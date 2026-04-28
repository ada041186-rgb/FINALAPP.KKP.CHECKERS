using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IGameSnapshotService
    {
        GameSnapshot Build(IGameContext ctx, int totalMoves);
        void Restore(IGameContext ctx, GameSnapshot snap);
    }
}