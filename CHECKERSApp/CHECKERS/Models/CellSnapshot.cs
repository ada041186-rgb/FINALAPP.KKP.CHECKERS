using CHECKERS.Models;

namespace CHECKERS.Models
{
    public class CellSnapshot
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CellValueEnum Value { get; set; }
    }
}