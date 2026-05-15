using CHECKERS.Models;

namespace CHECKERS.Services.AI
{
    public class HardAIStrategy : IAIStrategy
    {
        public AIDifficulty Difficulty => AIDifficulty.Hard;

        public Move ChooseMove(List<Move> availableMoves, Random rng, IMoveEvaluator moveEvaluator)
        {
            var captures = availableMoves.Where(m => m.Captured != null).ToList();

            if (captures.Count != 0)
            {
                return captures.OrderByDescending(moveEvaluator.Evaluate).First();
            }

            return availableMoves.OrderByDescending(moveEvaluator.Evaluate).First();
        }
    }
}
