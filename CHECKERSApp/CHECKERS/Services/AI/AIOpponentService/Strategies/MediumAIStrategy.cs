using CHECKERS.Models;

namespace CHECKERS.Services.AI
{
    public class MediumAIStrategy : IAIStrategy
    {
        public AIDifficulty Difficulty => AIDifficulty.Medium;

        public Move ChooseMove(List<Move> availableMoves, Random rng, IMoveEvaluator moveEvaluator)
        {
            var captures = availableMoves.Where(m => m.Captured != null).ToList();

            if (captures.Count != 0)
            {
                return captures[rng.Next(captures.Count)];
            }

            return availableMoves[rng.Next(availableMoves.Count)];
        }
    }
}
