using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IHintService
    {
        MoveHint? GetBestMove(Board board, CellValueEnum player);
    }
}