using CHECKERS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHECKERS.Models
{
    public class KingPiece : IPieceType
    {
        public static readonly KingPiece Instance = new();
        public IMoveStrategy GetStrategy() => new KingMoveStrategy();
    }
}