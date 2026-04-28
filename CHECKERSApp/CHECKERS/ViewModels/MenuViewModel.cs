using CHECKERS.Services;
using CHECKERS.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CHECKERS.ViewModels
{
    public class MenuViewModel : ViewModel
    {
        private readonly ISettingsDialogService _settingsDialog;

        public event System.Action? PlayRequested;

        public ICommand PlayCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand ExitCommand { get; }

        public MenuViewModel(ISettingsDialogService settingsDialog)
        {
            _settingsDialog = settingsDialog;

            PlayCommand = new Command(_ => PlayRequested?.Invoke());
            OpenSettingsCommand = new Command(_ => _settingsDialog.Show());
            ExitCommand = new Command(_ => Application.Current.Shutdown());
        }
    }
}