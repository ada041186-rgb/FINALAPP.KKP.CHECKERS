namespace CHECKERS.Models
{
    public class GameStatistics
    {
        public int TotalMoves { get; set; }
        public int WhiteCaptured { get; set; }
        public int BlackCaptured { get; set; }
        public int WhitePromotions { get; set; }
        public int BlackPromotions { get; set; }
    }
}