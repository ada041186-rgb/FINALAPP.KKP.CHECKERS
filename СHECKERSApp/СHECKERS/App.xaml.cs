using CHECKERS.Services;
using CHECKERS.View.Windows;
using CHECKERS.ViewModels;
using System.Windows;

namespace CHECKERS
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var registry = new MoveStrategyRegistry();
            var factory = new MoveStrategyFactory(registry);
            var rules = new GameRules(factory);
            var promotion = new PromotionService();
            var turnSwitcher = new TurnSwitcher();
            var afterMove = new AfterMoveHandler(turnSwitcher);
            var executor = new MoveExecutor(promotion, afterMove);
            var setup = new BoardSetupService();
            var ctx = new GameContext(rules, executor, setup);
            var dialog = new DialogService();

            var viewModel = new MainWindowViewModel(ctx, dialog);
            var window = new MainWindow { DataContext = viewModel };
            window.Show();
        }
    }
}