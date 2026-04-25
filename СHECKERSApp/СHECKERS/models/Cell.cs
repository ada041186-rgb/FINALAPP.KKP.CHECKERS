using CHECKERS.Models;
using CHECKERS.ViewModels.Base;

namespace CHECKERS.Models
{
    public class Cell : ViewModel
    {
        private CellValueEnum _cellValueEnum;
        private bool _act;
        private bool _isHighlighted; 

        public int Row { get; }
        public int Column { get; }

        public CellValueEnum Cellvalueenum
        {
            get => _cellValueEnum;
            set { _cellValueEnum = value; OnPropertyChanged(); }
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

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            _cellValueEnum = CellValueEnum.Empty;
        }

        public bool IsWhite => Cellvalueenum == CellValueEnum.WhiteChecker || Cellvalueenum == CellValueEnum.WhiteKing;
        public bool IsBlack => Cellvalueenum == CellValueEnum.BlackChecker || Cellvalueenum == CellValueEnum.BlackKing;
        public bool IsKing => Cellvalueenum == CellValueEnum.WhiteKing || Cellvalueenum == CellValueEnum.BlackKing;
        public bool IsEmpty => Cellvalueenum == CellValueEnum.Empty;

        public bool BelongsTo(CellValueEnum player) =>
            (player == CellValueEnum.WhiteChecker && IsWhite) ||
            (player == CellValueEnum.BlackChecker && IsBlack);
    }
}