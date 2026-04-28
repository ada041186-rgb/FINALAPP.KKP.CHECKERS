using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CHECKERS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IGameContext _ctx;
        private readonly IDialogService _dialog;
        private readonly IScreenNavigator _navigator;
        private readonly ISettingsDialogService _settingsDialog;
        private readonly IScoreService _score;
        private readonly IGameStatisticsService _statistics;
        private readonly IMoveHistoryService _history;
        private readonly IHintService _hint;
        private readonly ITurnTimerService _timer;
        private readonly IGameSaveService _save;

        private bool _isMenu = true;
        public bool IsMenu
        {
            get => _isMenu;
            set { _isMenu = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CellViewModel> _cells = new();
        public ObservableCollection<CellViewModel> Cells
        {
            get => _cells;
            set { _cells = value; OnPropertyChanged(); }
        }

        public int WhiteWins => _score.WhiteWins;
        public int BlackWins => _score.BlackWins;

        public int TotalMoves => _statistics.Current.TotalMoves;
        public int WhiteCaptured => _statistics.Current.WhiteCaptured;
        public int BlackCaptured => _statistics.Current.BlackCaptured;

        public ObservableCollection<string> MoveLog { get; } = new();

        private int _secondsLeft;
        public int SecondsLeft
        {
            get => _secondsLeft;
            private set { _secondsLeft = value; OnPropertyChanged(); }
        }

        private string _hintText = "";
        public string HintText
        {
            get => _hintText;
            private set { _hintText = value; OnPropertyChanged(); }
        }

        public string CurrentPlayerText =>
            _ctx.CurrentPlayer == CellValueEnum.WhiteChecker
                ? "Хід:  Білі"
                : "Хід:  Чорні";

        public ICommand PlayCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand BackToMenuCommand { get; }
        public ICommand CellCommand { get; }
        public ICommand HintCommand { get; }
        public ICommand SaveGameCommand { get; }
        public ICommand LoadGameCommand { get; }

        public MainWindowViewModel(
            IGameContext ctx,
            IDialogService dialog,
            IScreenNavigator navigator,
            ISettingsDialogService settingsDialog,
            IScoreService score,
            IGameStatisticsService statistics,
            IMoveHistoryService history,
            IHintService hint,
            ITurnTimerService timer,
            IGameSaveService save)
        {
            _ctx = ctx;
            _dialog = dialog;
            _navigator = navigator;
            _settingsDialog = settingsDialog;
            _score = score;
            _statistics = statistics;
            _history = history;
            _hint = hint;
            _timer = timer;
            _save = save;

            _timer.Tick += () =>
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SecondsLeft = _timer.SecondsLeft;
                });

            _timer.TimeExpired += () =>
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _dialog.ShowMessage("Час вийшов! Хід пропущено.", "Таймер");
                    SwitchTurnAfterTimeout();
                });

            PlayCommand = new Command(_ => StartGame());
            OpenSettingsCommand = new Command(_ => _settingsDialog.Show());
            ExitCommand = new Command(_ => Application.Current.Shutdown());
            NewGameCommand = new Command(_ => StartNewGame());
            BackToMenuCommand = new Command(_ => _navigator.GoToMenu());

            HintCommand = new Command(_ =>
            {
                var h = _hint.GetBestMove(_ctx.Board, _ctx.CurrentPlayer);
                HintText = h == null
                    ? "Немає доступних ходів"
                    : $"Підказка: ({h.Move.From.Row},{h.Move.From.Column}) → ({h.Move.To.Row},{h.Move.To.Column})";
            });

            SaveGameCommand = new Command(_ =>
            {
                var snapshot = BuildSnapshot();
                _save.Save(snapshot);
                _dialog.ShowMessage("Гру збережено.", "Збереження");
            });

            LoadGameCommand = new Command(_ =>
            {
                if (!_save.HasSave())
                {
                    _dialog.ShowMessage("Збереженої гри не знайдено.", "Завантаження");
                    return;
                }
                var snap = _save.Load();
                if (snap == null) return;
                RestoreSnapshot(snap);
                _navigator.GoToGame();
            });

            CellCommand = new Command(param =>
            {
                if (param is not CellViewModel cell) return;

                int movesCountBefore = _history.GetHistory().Count;

                _ctx.HandleClick(cell);

                if (_history.GetHistory().Count > movesCountBefore)
                {
                    RefreshAfterMove();
                }
                else
                {
                    HintText = "";
                    RefreshAllProperties();
                }
            });
        }

        private void StartGame()
        {
            StartNewGame();
            _navigator.GoToGame();
        }

        private void StartNewGame()
        {
            _timer.Stop();
            _ctx.NewGame();
            _statistics.Reset();
            _history.Clear();
            MoveLog.Clear();
            HintText = "";

            Cells = new ObservableCollection<CellViewModel>(_ctx.GetCellViewModels());
            RefreshAllProperties();
            _timer.Start();
        }

        private void RefreshAfterMove()
        {
            MoveLog.Clear();
            var all = _history.GetHistory();
            for (int i = 0; i < all.Count; i++)
            {
                var m = all[i];
                string side = (i % 2 == 0) ? "Білі" : "Чорні";
                string capture = m.Captured != null ? " ×" : "";
                MoveLog.Add($"{i + 1}. {side}: ({m.From.Row},{m.From.Column})→({m.To.Row},{m.To.Column}){capture}");
            }

            HintText = "";
            RefreshAllProperties();

            if (_ctx.GameOver)
            {
                _timer.Stop();
                var winner = _ctx.GetWinner()!.Value;
                _score.RecordWin(winner);
                string name = winner == CellValueEnum.WhiteChecker ? "Білі" : "Чорні";
                _dialog.ShowMessage($"Переможець — {name}!", "Кінець гри");
                OnPropertyChanged(nameof(WhiteWins));
                OnPropertyChanged(nameof(BlackWins));
                StartNewGame();
                return;
            }

            _timer.Reset();
            _timer.Start();
        }

        private void SwitchTurnAfterTimeout()
        {
            _ctx.ClearHighlights();

            _ctx.CurrentPlayer = _ctx.CurrentPlayer == CellValueEnum.WhiteChecker
                ? CellValueEnum.BlackChecker
                : CellValueEnum.WhiteChecker;

            RefreshAllProperties();
            _timer.Reset();
            _timer.Start();
        }

        private void RefreshAllProperties()
        {
            OnPropertyChanged(nameof(CurrentPlayerText));
            OnPropertyChanged(nameof(TotalMoves));
            OnPropertyChanged(nameof(WhiteCaptured));
            OnPropertyChanged(nameof(BlackCaptured));
            SecondsLeft = _timer.SecondsLeft;
        }


        private GameSnapshot BuildSnapshot()
        {
            var snap = new GameSnapshot
            {
                CurrentPlayer = _ctx.CurrentPlayer,
                TotalMoves = _statistics.Current.TotalMoves
            };
            foreach (var cell in _ctx.Board)
                snap.Cells.Add(new CellSnapshot
                {
                    Row = cell.Row,
                    Column = cell.Column,
                    Value = cell.Cellvalueenum
                });
            return snap;
        }

        private void RestoreSnapshot(GameSnapshot snap)
        {
            _ctx.NewGame();
            _statistics.Reset();
            _history.Clear();
            MoveLog.Clear();

            foreach (var cs in snap.Cells)
                _ctx.Board[cs.Row, cs.Column].Cellvalueenum = cs.Value;

            if (_ctx is CHECKERS.Services.GameContext gc)
                gc.CurrentPlayer = snap.CurrentPlayer;

            Cells = new ObservableCollection<CellViewModel>(_ctx.GetCellViewModels());
            RefreshAllProperties();
            _timer.Start();
        }
    }
}