using CHECKERS.ViewModels;

namespace CHECKERS.Models
{
    public class Cell
    {
        public int Row { get; }
        public int Column { get; }
        public CellViewModel ViewModel { get; set; } = null!;

        public CellValueEnum Cellvalueenum
        {
            get => ViewModel.Cellvalueenum;
            set => ViewModel.Cellvalueenum = value;
        }

        public bool IsWhite => Cellvalueenum == CellValueEnum.WhiteChecker
                            || Cellvalueenum == CellValueEnum.WhiteKing;
        public bool IsBlack => Cellvalueenum == CellValueEnum.BlackChecker
                            || Cellvalueenum == CellValueEnum.BlackKing;
        public bool IsKing => Cellvalueenum == CellValueEnum.WhiteKing
                            || Cellvalueenum == CellValueEnum.BlackKing;
        public bool IsEmpty => Cellvalueenum == CellValueEnum.Empty;

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