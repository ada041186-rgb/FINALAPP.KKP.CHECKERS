using CHECKERS.Models;
using CHECKERS.Services.AI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CHECKERS.Services
{
    public class AIOpponentService : IAIOpponentService
    {
        private readonly IGameRules _rules;
        private readonly IMoveEvaluator _moveEvaluator;
        private readonly Random _rng = new();
        private readonly Dictionary<AIDifficulty, IAIStrategy> _strategies;

        public AIDifficulty Difficulty { get; set; } = AIDifficulty.Medium;

        public bool IsAITurn(CellValueEnum currentPlayer) =>
            currentPlayer == CellValueEnum.BlackChecker;

        public AIOpponentService(
            IGameRules rules, 
            IMoveEvaluator moveEvaluator, 
            IEnumerable<IAIStrategy> strategies
        )
        {
            _rules = rules;
            _moveEvaluator = moveEvaluator;
            _strategies = strategies.ToDictionary(s => s.Difficulty);
        }

        public Move? ChooseMove(Board board, CellValueEnum player)
        {
            var allMoves = board.GetPiecesFor(player)
                .SelectMany(c => _rules.GetAvailableMoves(board, c.ViewModel))
                .ToList();

            if (!allMoves.Any()) return null;

            if (_strategies.TryGetValue(Difficulty, out var strategy))
            {
                return strategy.ChooseMove(allMoves, _rng, _moveEvaluator);
            }

            return _strategies[AIDifficulty.Easy].ChooseMove(allMoves, _rng, _moveEvaluator); 
        }
    }
}