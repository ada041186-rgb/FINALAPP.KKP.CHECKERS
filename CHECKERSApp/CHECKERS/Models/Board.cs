using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CHECKERS.Models
{
    public class Board : IEnumerable<Cell>
    {
        public const int Size = 8;
        private readonly Cell[,] _area = new Cell[Size, Size];

        public Cell this[int row, int col] => _area[row, col];

        public Board()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    _area[r, c] = new Cell(r, c);
        }

        public bool InBounds(int row, int col) =>
            row >= 0 && row < Size && col >= 0 && col < Size;

        public IEnumerable<Cell> GetPiecesFor(CellValueEnum player) =>
            this.Where(c => c.BelongsTo(player));

        public bool HasOpponentPieces(CellValueEnum currentPlayer)
        {
            return this.Any(c => !c.IsEmpty && !c.BelongsTo(currentPlayer));
        }

        public IEnumerator<Cell> GetEnumerator() => _area.Cast<Cell>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}