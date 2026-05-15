using CHECKERS.Models;


namespace CHECKERS.Services.AI
{
    public interface IAIStrategy
    {
        AIDifficulty Difficulty { get; }
        Move ChooseMove(List<Move> availableMoves, Random rng, IMoveEvaluator moveEvaluator);
    }
}
