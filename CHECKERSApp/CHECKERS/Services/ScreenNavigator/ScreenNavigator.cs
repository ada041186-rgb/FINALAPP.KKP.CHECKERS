using System;

namespace CHECKERS.Services
{
    public class ScreenNavigator : IScreenNavigator
    {
        private readonly Action _goToMenu;
        private readonly Action _goToGame;

        public ScreenNavigator(Action goToMenu, Action goToGame)
        {
            _goToMenu = goToMenu;
            _goToGame = goToGame;
        }

        public void GoToMenu() => _goToMenu();
        public void GoToGame() => _goToGame();
    }
}