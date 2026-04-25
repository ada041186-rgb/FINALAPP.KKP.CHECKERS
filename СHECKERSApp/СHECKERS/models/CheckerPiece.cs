using CHECKERS.Services;

namespace CHECKERS.Models
{
    public class CheckerPiece : IPieceType
    {
        public static readonly CheckerPiece Instance = new();
        public IMoveStrategy GetStrategy() => new CheckerMoveStrategy();
    }
}
