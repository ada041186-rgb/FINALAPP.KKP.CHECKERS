using CHECKERS.ViewModels;

namespace CHECKERS.Models
{
    public class Cell
    {
        private CellValueEnum _cellvalueenum = CellValueEnum.Empty;

        public int Row { get; }
        public int Column { get; }
        public CellViewModel ViewModel { get; set; } = null!;

        public CellValueEnum Cellvalueenum
        {
            get => _cellvalueenum;
            set
            {
                _cellvalueenum = value;
                ViewModel?.Refresh();
            }
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

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}