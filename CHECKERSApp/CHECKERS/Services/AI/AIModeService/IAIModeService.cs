namespace CHECKERS.Services
{
    public interface IAIModeService
    {
        bool IsEnabled { get; }
        void Enable();
        void Disable();
        void Toggle();
    }
}