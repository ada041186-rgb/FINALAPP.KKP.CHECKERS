namespace CHECKERS.Services
{
    public class AIModeService : IAIModeService
    {
        public bool IsEnabled { get; private set; }

        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;
        public void Toggle() => IsEnabled = !IsEnabled;
    }
}