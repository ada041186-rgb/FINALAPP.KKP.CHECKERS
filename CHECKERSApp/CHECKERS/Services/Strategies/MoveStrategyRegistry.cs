using CHECKERS.ViewModels;
using System;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public class MoveStrategyRegistry
    {
        private readonly List<(Func<CellViewModel, bool> Condition, IMoveStrategy Strategy)> _rules = new();
        private readonly IMoveStrategy _default = new CheckerMoveStrategy();

        public MoveStrategyRegistry()
        {
            Register(cell => cell.IsKing, new KingMoveStrategy());
        }

        public void Register(Func<CellViewModel, bool> condition, IMoveStrategy strategy)
        {
            _rules.Add((condition, strategy));
        }

        public IMoveStrategy Resolve(CellViewModel cell)
        {
            foreach (var rule in _rules)
                if (rule.Condition(cell))
                    return rule.Strategy;
            return _default;
        }
    }
}