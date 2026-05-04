using CHECKERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHECKERS.Services
{
    public interface IMoveEvaluator
    {
        int Evaluate(Move move);
    }
}
