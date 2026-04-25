using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CHECKERS.Models;

namespace CHECKERS.Models
{
    public class Board : IEnumerable<Cell>
    {
        private readonly Cell[,] _area = new Cell[8, 8];

        public Cell this[int row, int col] => _area[row, col];

        public Board()
        {
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    _area[r, c] = new Cell(r, c);
        }

        public bool InBounds(int row, int col) =>
            row >= 0 && row < 8 && col >= 0 && col < 8;

        public bool HasOpponentPieces(CellValueEnum currentPlayer)
        {
            return this.Any(c =>
                (currentPlayer == CellValueEnum.WhiteChecker && c.IsBlack) ||
                (currentPlayer == CellValueEnum.BlackChecker && c.IsWhite));
        }

        public IEnumerator<Cell> GetEnumerator() =>
            _area.Cast<Cell>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}