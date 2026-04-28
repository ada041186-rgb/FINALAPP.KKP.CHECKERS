using CHECKERS.Models;
using CHECKERS.ViewModels;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public class GameContext : IGameContext, IStateContext
    {
        public Board Board { get; private set; } = new();
        public CellValueEnum CurrentPlayer { get; set; } = CellValueEnum.WhiteChecker;
        public CellViewModel? SelectedCell { get; set; }
        public List<Move> AvailableMoves { get; set; } = new();
        public IGameState State { get; private set; }

        public IGameRules Rules { get; }
        public IMoveExecutor MoveExecutor { get; }
        public bool GameOver { get; set; }

        private readonly IBoardSetupService _setup;
        private List<CellViewModel> _cellViewModels = new();

        public GameContext(IGameRules rules, IMoveExecutor moveExecutor, IBoardSetupService setup)
        {
            Rules = rules;
            MoveExecutor = moveExecutor;
            _setup = setup;
            State = new IdleState();
        }

        public void TransitionTo(IGameState state) => State = state;
        public void HandleClick(CellViewModel cell) => State.HandleCellClick(this, cell);

        public void NewGame()
        {
            var board = new Board();
            _cellViewModels = new List<CellViewModel>();

            foreach (var cell in board)
            {
                var vm = new CellViewModel(cell);
                cell.ViewModel = vm;
                _cellViewModels.Add(vm);
            }

            Board = board;
            CurrentPlayer = CellValueEnum.WhiteChecker;
            GameOver = false;
            SelectedCell = null;
            AvailableMoves.Clear();
            TransitionTo(new IdleState());
            _setup.Setup(board);
        }

        public IReadOnlyList<CellViewModel> GetCellViewModels() => _cellViewModels;
        public CellValueEnum? GetWinner() => GameOver ? CurrentPlayer : null;

        public void ClearHighlights()
        {
            foreach (var c in Board)
            {
                if (c.ViewModel == null) continue;
                c.ViewModel.Act = false;
                c.ViewModel.IsHighlighted = false;
            }
            SelectedCell = null;
            AvailableMoves.Clear();
        }
    }
}