using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHECKERS.Models
{
    public record Move(Cell From, Cell To, Cell? Captured = null);
}