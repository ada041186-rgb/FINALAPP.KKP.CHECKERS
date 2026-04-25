using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IAfterMoveHandler
    {
        void Handle(IStateContext ctx, Move move);
    }
}