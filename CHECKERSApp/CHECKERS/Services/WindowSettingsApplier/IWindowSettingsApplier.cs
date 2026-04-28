using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IWindowSettingsApplier
    {
        void Apply(AppSettings settings);
    }
}