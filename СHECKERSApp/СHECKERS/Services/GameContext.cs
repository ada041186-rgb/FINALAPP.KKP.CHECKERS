using CHECKERS.Models;
using System.Collections.Generic;

namespace CHECKERS.Services
{
    public class GameContext
    {
        public Board Board { get; set; } = new();
        public CellValueEnum CurrentPlayer { get; set; } = CellValueEnum.WhiteChecker;
        public Cell? SelectedCell { get; set; }
        public List<Move> AvailableMoves { get; set; } = new();
        public IGameState State { get; private set; }
        public GameRules Rules { get; } = new();
        public bool GameOver { get; set; }

        public GameContext()
        {
            State = new IdleState();
        }

        public void TransitionTo(IGameState state) => State = state;

        public void HandleClick(Cell cell) => State.HandleCellClick(this, cell);

        public void ClearHighlights()
        {
            foreach (var c in Board) { c.Act = false; c.IsHighlighted = false; }
            AvailableMoves.Clear();
        }
    }
}