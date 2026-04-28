using CHECKERS.Services.Settings;
using CHECKERS.View.Windows;
using CHECKERS.ViewModels;
using System.Windows;

namespace CHECKERS.Services
{
    public class SettingsDialogService : ISettingsDialogService
    {
        private readonly ISettingsService _settingsService;
        private readonly IWindowSettingsApplier _applier;

        public SettingsDialogService(ISettingsService settingsService,
                                     IWindowSettingsApplier applier)
        {
            _settingsService = settingsService;
            _applier = applier;
        }

        public void Show()
        {
            var vm = new SettingsViewModel(_settingsService);
            var win = new SettingsWindow(vm)
            {
                Owner = Application.Current.MainWindow
            };
            win.ShowDialog();

            if (vm.DialogResult)
                _applier.Apply(_settingsService.Load());
        }
    }
}