using CHECKERS.Models;
using CHECKERS.ViewModels.Base;

namespace CHECKERS.ViewModels
{
    public class CellViewModel : ViewModel
    {
        private bool _act;
        private bool _isHighlighted;

        public Cell Cell { get; }
        public int Row => Cell.Row;
        public int Column => Cell.Column;

        public CellValueEnum Cellvalueenum
        {
            get => Cell.Cellvalueenum;
            set => Cell.Cellvalueenum = value;
        }

        public bool IsWhite => Cell.IsWhite;
        public bool IsBlack => Cell.IsBlack;
        public bool IsKing => Cell.IsKing;
        public bool IsEmpty => Cell.IsEmpty;

        public bool Act
        {
            get => _act;
            set { _act = value; OnPropertyChanged(); }
        }

        public bool IsHighlighted
        {
            get => _isHighlighted;
            set { _isHighlighted = value; OnPropertyChanged(); }
        }

        public bool BelongsTo(CellValueEnum player) => Cell.BelongsTo(player);

        public CellViewModel(Cell cell)
        {
            Cell = cell;
            cell.ViewModel = this;
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(Cellvalueenum));
            OnPropertyChanged(nameof(IsWhite));
            OnPropertyChanged(nameof(IsBlack));
            OnPropertyChanged(nameof(IsKing));
            OnPropertyChanged(nameof(IsEmpty));
        }
    }
}