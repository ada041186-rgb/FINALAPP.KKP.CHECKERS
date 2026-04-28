using CHECKERS.Models;
using CHECKERS.Services.States;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public interface IStateContext : IBoardContext, IStateManager, IMoveContext { }
}