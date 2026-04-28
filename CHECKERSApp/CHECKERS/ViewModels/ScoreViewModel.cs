using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.ViewModels.Base;

namespace CHECKERS.ViewModels
{
    public class ScoreViewModel : ViewModel
    {
        private readonly IScoreService _score;

        public int WhiteWins => _score.WhiteWins;
        public int BlackWins => _score.BlackWins;

        public ScoreViewModel(IScoreService score)
        {
            _score = score;
        }

        public void RecordWin(CellValueEnum winner)
        {
            _score.RecordWin(winner);
            OnPropertyChanged(nameof(WhiteWins));
            OnPropertyChanged(nameof(BlackWins));
        }

        public void Reset()
        {
            _score.Reset();
            OnPropertyChanged(nameof(WhiteWins));
            OnPropertyChanged(nameof(BlackWins));
        }
    }
}