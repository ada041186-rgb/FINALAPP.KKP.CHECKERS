using CHECKERS.Models;

namespace CHECKERS.Services.Settings
{
    public interface ISettingsService
    {
        AppSettings Load();
        void Save(AppSettings settings);
    }
}