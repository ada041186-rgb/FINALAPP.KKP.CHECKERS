using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHECKERS.Services.States
{
    public interface IMoveContext
    {
        IMoveExecutor MoveExecutor { get; }
    }
}
