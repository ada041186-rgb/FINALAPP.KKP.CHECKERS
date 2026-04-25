using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IMoveExecutor
    {
        void Execute(IStateContext ctx, Move move);
    }
}