using CHECKERS.Models;

namespace CHECKERS.Services.AI
{
    public class EasyAIStrategy : IAIStrategy
    {
        public AIDifficulty Difficulty => AIDifficulty.Easy;

        public Move ChooseMove(List<Move> availableMoves, Random rng, IMoveEvaluator moveEvaluator) =>
            availableMoves[rng.Next(availableMoves.Count)];
    }
}
