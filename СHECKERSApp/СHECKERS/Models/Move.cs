using CHECKERS.ViewModels;

namespace CHECKERS.Models
{
    public record Move(CellViewModel From, CellViewModel To, CellViewModel? Captured = null);
}