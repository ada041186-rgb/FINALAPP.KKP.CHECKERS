using CHECKERS.Models;

namespace CHECKERS.Services
{
    public static class MoveStrategyFactory
    {
        private static readonly IMoveStrategy Checker = new CheckerMoveStrategy();
        private static readonly IMoveStrategy King = new KingMoveStrategy();

        public static IMoveStrategy Get(Cell cell) =>
            cell.IsKing ? King : Checker;
    }
}