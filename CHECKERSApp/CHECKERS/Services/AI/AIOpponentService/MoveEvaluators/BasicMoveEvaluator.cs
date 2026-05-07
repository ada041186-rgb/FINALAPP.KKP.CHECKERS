using CHECKERS.Models;

namespace CHECKERS.Services.AI
{
    public class BasicMoveEvaluator : IMoveEvaluator
    {
        private const int PromotionBonus = 15;
        private const int EdgeBonus = 5;
        private const int BaseRowPenalty = 4;
        private const int CenterBonus = 3;

        public int Evaluate(Move move)
        {
            int score = 0;

            score += EvaluateForwardProgress(move);

            if (IsPromotion(move))
                score += PromotionBonus;

            if (IsEdge(move))
                score += EdgeBonus;

            if (IsFromBaseRow(move))
                score -= BaseRowPenalty;

            if (IsCenter(move))
                score += CenterBonus;

            return score;
        }

        private bool IsPromotion(Move move)
        {
            return move.From.IsWhite
                ? move.To.Row == 0
                : move.To.Row == Board.Size - 1;
        }

        private bool IsEdge(Move move)
        {
            return move.To.Column == 0 || move.To.Column == Board.Size - 1;
        }

        private bool IsFromBaseRow(Move move)
        {
            int baseRow = move.From.IsWhite ? Board.Size - 1 : 0;
            return move.From.Row == baseRow;
        }

        private bool IsCenter(Move move)
        {
            int mid = Board.Size / 2;
            return (move.To.Row == mid || move.To.Row == mid - 1) &&
                    (move.To.Column == mid || move.To.Column == mid - 1);
        }

        private int EvaluateForwardProgress(Move move)
        {
            return move.From.IsWhite
                ? (Board.Size - 1 - move.To.Row)
                : move.To.Row;
        }
    }
}
