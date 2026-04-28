using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.ViewModels.Base;

namespace CHECKERS.ViewModels
{
    public class StatisticsViewModel : ViewModel
    {
        private readonly IGameStatisticsService _stats;

        public int TotalMoves => _stats.Current.TotalMoves;
        public int WhiteCaptured => _stats.Current.WhiteCaptured;
        public int BlackCaptured => _stats.Current.BlackCaptured;
        public int WhitePromotions => _stats.Current.WhitePromotions;
        public int BlackPromotions => _stats.Current.BlackPromotions;

        public StatisticsViewModel(IGameStatisticsService stats)
        {
            _stats = stats;
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(TotalMoves));
            OnPropertyChanged(nameof(WhiteCaptured));
            OnPropertyChanged(nameof(BlackCaptured));
            OnPropertyChanged(nameof(WhitePromotions));
            OnPropertyChanged(nameof(BlackPromotions));
        }

        public void Reset()
        {
            _stats.Reset();
            Refresh();
        }
    }
}