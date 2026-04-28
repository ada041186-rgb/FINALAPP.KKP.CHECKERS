using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Models
{
    public class GameSnapshot
    {
        public List<CellSnapshot> Cells { get; set; } = new();
        public CellValueEnum CurrentPlayer { get; set; }
        public int TotalMoves { get; set; }
    }
}