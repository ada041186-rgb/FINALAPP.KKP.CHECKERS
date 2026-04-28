using CHECKERS.Services.Settings;
using CHECKERS.View.Windows;
using CHECKERS.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CHECKERS.ViewModels
{
    public class MenuViewModel : ViewModel
    {
        private readonly ISettingsService _settingsService;

        public event System.Action? PlayRequested;

        public ICommand PlayCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand ExitCommand { get; }

        public MenuViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            PlayCommand = new Command(_ => PlayRequested?.Invoke());
            OpenSettingsCommand = new Command(_ => OpenSettings());
            ExitCommand = new Command(_ => Application.Current.Shutdown());
        }

        private void OpenSettings()
        {
            var vm = new SettingsViewModel(_settingsService);
            var win = new SettingsWindow(vm)
            {
                Owner = Application.Current.MainWindow
            };
            win.ShowDialog();

            if (vm.DialogResult)
                ApplySettings();
        }

        private void ApplySettings()
        {
            var s = _settingsService.Load();
            var w = Application.Current.MainWindow;
            if (w == null) return;

            if (s.IsFullScreen)
            {
                w.WindowStyle = WindowStyle.None;
                w.WindowState = WindowState.Maximized;
            }
            else
            {
                w.WindowStyle = WindowStyle.SingleBorderWindow;
                w.WindowState = WindowState.Normal;

                var res = s.Resolution.Split('×');
                if (res.Length == 2
                    && double.TryParse(res[0].Trim(), out double width)
                    && double.TryParse(res[1].Trim().Split(' ')[0], out double height))
                {
                    w.Width = width;
                    w.Height = height;
                }
            }
        }
    }
}