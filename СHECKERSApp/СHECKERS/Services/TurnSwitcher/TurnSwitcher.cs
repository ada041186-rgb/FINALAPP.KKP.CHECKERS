using CHECKERS.Models;

namespace CHECKERS.Services
{
    public class TurnSwitcher : ITurnSwitcher
    {
        public void Switch(IStateContext ctx)
        {
            ctx.CurrentPlayer = ctx.CurrentPlayer == CellValueEnum.WhiteChecker
                ? CellValueEnum.BlackChecker
                : CellValueEnum.WhiteChecker;
        }
    }
}