using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.Services.Settings;
using CHECKERS.ViewModels.Base;
using System; 
using System.Collections.Generic;
using System.Linq; 
using System.Windows.Input;

namespace CHECKERS.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        private readonly ISettingsService _settingsService;
        private readonly IAIOpponentService _ai;

        public List<ResolutionOption> Resolutions { get; } = new()
        {
            new ResolutionOption("560 × 620  (замовчування)", 560,  620),
            new ResolutionOption("800 × 800",                 800,  800),
            new ResolutionOption("1024 × 768",               1024,  768),
            new ResolutionOption("1280 × 720",               1280,  720),
            new ResolutionOption("1280 × 1024",              1280, 1024),
            new ResolutionOption("1600 × 900",               1600,  900),
            new ResolutionOption("1920 × 1080",              1920, 1080),
        };

        private ResolutionOption _selectedResolution = null!;
        public ResolutionOption SelectedResolution
        {
            get => _selectedResolution;
            set { _selectedResolution = value; OnPropertyChanged(); }
        }

        private bool _isFullScreen;
        public bool IsFullScreen
        {
            get => _isFullScreen;
            set { _isFullScreen = value; OnPropertyChanged(); }
        }

        public IEnumerable<AIDifficulty> Difficulties => Enum.GetValues(typeof(AIDifficulty)).Cast<AIDifficulty>();

        public bool DialogResult { get; private set; }

        private AIDifficulty _selectedDifficulty;
        public AIDifficulty SelectedDifficulty
        {
            get => _selectedDifficulty;
            set { _selectedDifficulty = value; OnPropertyChanged(); }
        }

        public event System.Action? RequestClose;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public SettingsViewModel(ISettingsService settingsService, IAIOpponentService ai)
        {
            _settingsService = settingsService;
            _ai = ai;

            var s = _settingsService.Load();

            _selectedResolution = Resolutions.Find(r => r.Label.StartsWith(s.Resolution))
                                  ?? Resolutions[0];
            _isFullScreen = s.IsFullScreen;

            SelectedDifficulty = _ai.Difficulty;

            SaveCommand = new Command(_ => Save());
            CancelCommand = new Command(_ => Cancel());
        }

        private void Save()
        {
            _settingsService.Save(new AppSettings
            {
                Resolution = SelectedResolution.Label,
                IsFullScreen = IsFullScreen
            });

            _ai.Difficulty = SelectedDifficulty;

            DialogResult = true;
            RequestClose?.Invoke();
        }

        private void Cancel()
        {
            DialogResult = false;
            RequestClose?.Invoke();
        }
    }
}