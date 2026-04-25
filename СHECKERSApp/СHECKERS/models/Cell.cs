using СHECKERS.ViewModels.Base;

namespace СHECKERS.Models
{
    public class Cell : ViewModel
    {
        private CellValueEnum cellValueEnum;
        private bool act;
        private int row;
        private int column;
        private Board? board;

        public CellValueEnum Cellvalueenum
        {
            get => cellValueEnum;
            set
            {
                cellValueEnum = value;
                OnPropertyChanged();
            }
        }

        public bool Act
        {
            get => act;
            set
            {
                act = value;
                OnPropertyChanged();
            }
        }

        public int Row
        {
            get => row;
            set
            {
                row = value;
                OnPropertyChanged();
            }
        }

        public int Column
        {
            get => column;
            set
            {
                column = value;
                OnPropertyChanged();
            }
        }

        public Board? Board
        {
            get => board;
            set
            {
                board = value;
                OnPropertyChanged();
            }
        }

        public Cell(int row, int column, Board board)
        {
            Row = row;
            Column = column;
            Board = board;
            cellValueEnum = CellValueEnum.Empty;
        }
    }
}