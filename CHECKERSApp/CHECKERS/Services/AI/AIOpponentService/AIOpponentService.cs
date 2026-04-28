using CHECKERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CHECKERS.Services
{
    public class AIOpponentService : IAIOpponentService
    {
        private readonly IGameRules _rules;
        private readonly Random _rng = new();

        public AIDifficulty Difficulty { get; set; } = AIDifficulty.Medium;

        public bool IsAITurn(CellValueEnum currentPlayer) =>
            currentPlayer == CellValueEnum.BlackChecker;

        public AIOpponentService(IGameRules rules)
        {
            _rules = rules;
        }

        public Move? ChooseMove(Board board, CellValueEnum player)
        {
            var all = CollectAllMoves(board, player);
            if (!all.Any()) return null;

            return Difficulty switch
            {
                AIDifficulty.Easy => PickEasy(all),
                AIDifficulty.Medium => PickMedium(all),
                AIDifficulty.Hard => PickHard(all), 
                _ => PickEasy(all)
            };
        }

        private List<Move> CollectAllMoves(Board board, CellValueEnum player)
        {
            return board
                .Where(c => c.BelongsTo(player))
                .SelectMany(c => _rules.GetAvailableMoves(board, c.ViewModel))
                .ToList();
        }


        private Move PickEasy(List<Move> moves) =>
            moves[_rng.Next(moves.Count)];

        private Move PickMedium(List<Move> moves)
        {
            var captures = moves.Where(m => m.Captured != null).ToList();
            return captures.Any()
                ? captures[_rng.Next(captures.Count)]
                : PickEasy(moves);
        }

        private Move PickHard(List<Move> moves)
        {
            var captures = moves.Where(m => m.Captured != null).ToList();

            if (captures.Any())
                return captures.OrderByDescending(m => EvaluateMove(m)).First();

            return moves.OrderByDescending(m => EvaluateMove(m)).First();
        }


        private int EvaluateMove(Move move)
        {
            int score = 0;

            score += move.To.Row;

            if (move.To.Row == 7) score += 10;

            if (move.To.Column == 0 || move.To.Column == 7)
                score += 3;

            if (move.From.Row == 0)
                score -= 4;

            if (move.To.Column == 3 || move.To.Column == 4)
                score += 2;

            return score;
        }
    }
}