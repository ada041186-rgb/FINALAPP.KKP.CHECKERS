using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CHECKERS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IGameContext _ctx;
        private readonly IDialogService _dialog;
        private ICommand? _cellCommand;
        private ICommand? _newGameCommand;
        private ObservableCollection<CellViewModel> _cells = new();

        public ObservableCollection<CellViewModel> Cells
        {
            get => _cells;
            set { _cells = value; OnPropertyChanged(); }
        }

        public string CurrentPlayerText =>
            _ctx.CurrentPlayer == CellValueEnum.WhiteChecker
                ? "Хід:  Білі"
                : "Хід:  Чорні";

        public ICommand NewGameCommand =>
            _newGameCommand ??= new Command(_ => StartNewGame());

        public ICommand CellCommand => _cellCommand ??= new Command(param =>
        {
            if (param is not CellViewModel cell) return;

            _ctx.HandleClick(cell);
            OnPropertyChanged(nameof(CurrentPlayerText));

            if (_ctx.GameOver)
            {
                var winner = _ctx.GetWinner();
                string winnerName = winner == CellValueEnum.WhiteChecker
                    ? "Білі" : "Чорні";

                _dialog.ShowMessage($"Переможець — {winnerName}!", "Кінець гри");
                StartNewGame();
            }
        });

        private void StartNewGame()
        {
            _ctx.NewGame();
            Cells = new ObservableCollection<CellViewModel>(_ctx.GetCellViewModels());
            OnPropertyChanged(nameof(CurrentPlayerText));
        }

        public MainWindowViewModel(IGameContext ctx, IDialogService dialog)
        {
            _ctx = ctx;
            _dialog = dialog;
            StartNewGame();
        }
    }
}