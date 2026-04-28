using CHECKERS.Services;

namespace CHECKERS.Models
{
    public interface IPieceType
    {
        IMoveStrategy GetStrategy();
    }
}