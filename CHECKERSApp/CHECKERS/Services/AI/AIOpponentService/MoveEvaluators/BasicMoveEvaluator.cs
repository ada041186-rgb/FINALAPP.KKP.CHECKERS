using CHECKERS.Models;
using CHECKERS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CHECKERS.Services.AI
{
    public class BasicMoveEvaluator : IMoveEvaluator
    {
        private const int PromotionBonus = 10;
        private const int EdgeBonus = 3;
        private const int StartRowPenalty = -4;
        private const int CenterBonus = 2;

        public int Evaluate(Move move)
        {
            int score = 0;

            score += EvaluateForwardProgress(move);

            if (IsPromotion(move))
                score += PromotionBonus;

            if (IsEdge(move))
                score += EdgeBonus;

            if (IsFromStartRow(move))
                score -= StartRowPenalty;

            if (IsCenter(move))
                score += CenterBonus;

            return score;
        }

        private bool IsCenter(Move move)
        {
            return move.To.Row == 7;
        }

        private bool IsFromStartRow(Move move)
        {
            return move.To.Column == 0 || move.To.Column == 7;
        }

        private bool IsEdge(Move move)
        {
            return move.From.Row == 0;
        }

        private bool IsPromotion(Move move)
        {
            return move.To.Column == 3 || move.To.Column == 4;
        }

        private int EvaluateForwardProgress(Move move)
        {
            return move.To.Row;
        }
    }
}
