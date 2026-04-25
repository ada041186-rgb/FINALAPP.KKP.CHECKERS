using CHECKERS.ViewModels;

namespace CHECKERS.Services
{
    public class MoveStrategyFactory : IMoveStrategyFactory
    {
        private readonly MoveStrategyRegistry _registry;

        public MoveStrategyFactory(MoveStrategyRegistry registry)
        {
            _registry = registry;
        }

        public IMoveStrategy Get(CellViewModel cell) =>
            _registry.Resolve(cell);
    }
}