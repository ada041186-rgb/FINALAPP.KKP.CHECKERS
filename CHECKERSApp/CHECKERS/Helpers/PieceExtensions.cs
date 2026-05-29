using CHECKERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHECKERS.Helpers
{
    internal static class PieceExtensions
    {
        public static bool IsWhite(this CellValueEnum value) =>
            value == CellValueEnum.WhiteChecker || value == CellValueEnum.WhiteKing;

        public static bool IsBlack(this CellValueEnum value) =>
            value == CellValueEnum.BlackChecker || value == CellValueEnum.BlackKing;
    }
}
