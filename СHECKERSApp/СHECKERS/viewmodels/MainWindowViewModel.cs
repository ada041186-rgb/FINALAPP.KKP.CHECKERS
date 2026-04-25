using CHECKERS.Models;
using CHECKERS.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CHECKERS.ViewModels.Base;

namespace CHECKERS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly GameContext _ctx = new();
        private readonly BoardSetupService _setup = new();
        private ICommand? _cellCommand;
        private ICommand? _newGameCommand;
        private ObservableCollection<Cell> _cells = new();

        public ObservableCollection<Cell> Cells
        {
            get => _cells;
            set { _cells = value; OnPropertyChanged(); }
        }

        public string CurrentPlayerText =>
            _ctx.CurrentPlayer == CellValueEnum.WhiteChecker ? "Хід:  Білі" : "Хід:  Чорні";

        public ICommand NewGameCommand => _newGameCommand ??= new Command(_ => StartNewGame());

        public ICommand CellCommand => _cellCommand ??= new Command(param =>
        {
            if (param is not Cell cell) return;

            _ctx.HandleClick(cell);
            OnPropertyChanged(nameof(CurrentPlayerText));

            if (_ctx.GameOver)
            {
                string winner = _ctx.CurrentPlayer == CellValueEnum.WhiteChecker ? "Білі " : "Чорні ";
                MessageBox.Show($"Переможець — {winner}!", "Кінець гри",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                StartNewGame();
            }
        });

        private void StartNewGame()
        {
            _ctx.ClearHighlights();
            _ctx.Board = new Board();
            _ctx.CurrentPlayer = CellValueEnum.WhiteChecker;
            _ctx.GameOver = false;
            _ctx.SelectedCell = null;
            _ctx.TransitionTo(new IdleState());
            _setup.Setup(_ctx.Board);
            Cells = new ObservableCollection<Cell>(_ctx.Board);
            OnPropertyChanged(nameof(CurrentPlayerText));
        }

        public MainWindowViewModel() => StartNewGame();
    }
}