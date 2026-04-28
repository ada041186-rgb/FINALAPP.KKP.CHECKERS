using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CHECKERS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IGameContext _ctx;
        private readonly IDialogService _dialog;
        private readonly IScreenNavigator _navigator;
        private readonly ISettingsDialogService _settingsDialog;

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

        public MainWindowViewModel(IGameContext ctx,
                                   IDialogService dialog,
                                   IScreenNavigator navigator,
                                   ISettingsDialogService settingsDialog)
        {
            _ctx = ctx;
            _dialog = dialog;
            _navigator = navigator;
            _settingsDialog = settingsDialog;

            PlayCommand = new Command(_ => StartGame());
            OpenSettingsCommand = new Command(_ => _settingsDialog.Show());
            ExitCommand = new Command(_ => Application.Current.Shutdown());
            NewGameCommand = new Command(_ => StartNewGame());
            BackToMenuCommand = new Command(_ => _navigator.GoToMenu());

            CellCommand = new Command(param =>
            {
                if (param is not CellViewModel cell) return;

                _ctx.HandleClick(cell);
                OnPropertyChanged(nameof(CurrentPlayerText));

                if (!_ctx.GameOver) return;

                var winner = _ctx.GetWinner();
                string name = winner == CellValueEnum.WhiteChecker ? "Білі" : "Чорні";
                _dialog.ShowMessage($"Переможець — {name}!", "Кінець гри");
                StartNewGame();
            });
        }

        private void StartGame()
        {
            StartNewGame();
            _navigator.GoToGame();
        }

        private void StartNewGame()
        {
            _ctx.NewGame();
            Cells = new ObservableCollection<CellViewModel>(_ctx.GetCellViewModels());
            OnPropertyChanged(nameof(CurrentPlayerText));
        }
    }
}