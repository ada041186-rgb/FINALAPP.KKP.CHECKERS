using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.Services.AI;
using CHECKERS.Services.Settings;
using CHECKERS.View.Windows;
using CHECKERS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CHECKERS
{
    public partial class App : Application
    {
        private ServiceProvider? _provider;
        private MainWindow? _mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _provider = BuildServices().BuildServiceProvider();

            var settings = _provider.GetRequiredService<ISettingsService>().Load();
            var applier = _provider.GetRequiredService<IWindowSettingsApplier>();
            var vm = _provider.GetRequiredService<MainWindowViewModel>();

            _mainWindow = new MainWindow { DataContext = vm };
            Application.Current.MainWindow = _mainWindow;
            applier.Apply(settings);
            _mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _provider?.GetService<ITurnTimerService>()?.Stop();

            if (_mainWindow != null)
            {
                var svc = _provider?.GetService<ISettingsService>();
                if (svc != null)
                {
                    var s = svc.Load();
                    s.IsFullScreen = _mainWindow.WindowStyle == WindowStyle.None;
                    svc.Save(s);
                }
            }
            _provider?.Dispose();
            base.OnExit(e);
        }

        private static IServiceCollection BuildServices()
        {
            var s = new ServiceCollection();
            s.AddSingleton<IAIOpponentService, AIOpponentService>();
            s.AddSingleton<IAIModeService, AIModeService>();
            s.AddSingleton<IGameSnapshotService, GameSnapshotService>();
            s.AddSingleton<ISettingsService, SettingsService>();
            s.AddSingleton<IWindowSettingsApplier, WindowSettingsApplier>();
            s.AddSingleton<IDialogService, DialogService>();
            s.AddSingleton<ISettingsDialogService, SettingsDialogService>();
            s.AddSingleton<MoveStrategyRegistry>();
            s.AddSingleton<IMoveStrategyFactory, MoveStrategyFactory>();
            s.AddSingleton<IGameRules, GameRules>();
            s.AddSingleton<IPromotionService, PromotionService>();
            s.AddSingleton<ITurnSwitcher, TurnSwitcher>();
            s.AddSingleton<IAfterMoveHandler, AfterMoveHandler>();
            s.AddSingleton<IMoveExecutor, MoveExecutor>();
            s.AddSingleton<IBoardSetupService, BoardSetupService>();
            s.AddSingleton<IGameContext, GameContext>();

            s.AddSingleton<IMoveHistoryService, MoveHistoryService>();
            s.AddSingleton<IScoreService, ScoreService>();
            s.AddSingleton<IGameStatisticsService, GameStatisticsService>();
            s.AddSingleton<IHintService, HintService>();
            s.AddSingleton<IGameSaveService, GameSaveService>();
            s.AddSingleton<ITurnTimerService>(_ => new TurnTimerService(60));

            s.AddSingleton<IMoveEvaluator, BasicMoveEvaluator>();

            s.AddSingleton<IScreenNavigator>(sp =>
            {
                var lazy = new Lazy<MainWindowViewModel>(
                    () => sp.GetRequiredService<MainWindowViewModel>());
                return new ScreenNavigator(
                    goToMenu: () => lazy.Value.IsMenu = true,
                    goToGame: () => lazy.Value.IsMenu = false);
            });

            s.AddSingleton<MainWindowViewModel>();
            return s;
        }
    }
}