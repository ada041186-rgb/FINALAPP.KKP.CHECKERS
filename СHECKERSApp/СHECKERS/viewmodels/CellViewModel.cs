using CHECKERS.Models;
using CHECKERS.ViewModels.Base;

namespace CHECKERS.ViewModels
{
    public class CellViewModel : ViewModel
    {
        private bool _act;
        private bool _isHighlighted;
        private CellValueEnum _cellvalueenum;

        public Cell Cell { get; }
        public int Row => Cell.Row;
        public int Column => Cell.Column;

        public CellValueEnum Cellvalueenum
        {
            get => _cellvalueenum;
            set
            {
                _cellvalueenum = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsWhite));
                OnPropertyChanged(nameof(IsBlack));
                OnPropertyChanged(nameof(IsKing));
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

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

        public bool IsWhite => _cellvalueenum == CellValueEnum.WhiteChecker
                            || _cellvalueenum == CellValueEnum.WhiteKing;
        public bool IsBlack => _cellvalueenum == CellValueEnum.BlackChecker
                            || _cellvalueenum == CellValueEnum.BlackKing;
        public bool IsKing => _cellvalueenum == CellValueEnum.WhiteKing
                            || _cellvalueenum == CellValueEnum.BlackKing;
        public bool IsEmpty => _cellvalueenum == CellValueEnum.Empty;

        public bool BelongsTo(CellValueEnum player) =>
            (player == CellValueEnum.WhiteChecker && IsWhite) ||
            (player == CellValueEnum.BlackChecker && IsBlack);

        public CellViewModel(Cell cell)
        {
            Cell = cell;
            _cellvalueenum = CellValueEnum.Empty;
        }
    }
}