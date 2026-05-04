using CHECKERS.Models;
using System.Windows;

namespace CHECKERS.Services
{
    public class WindowSettingsApplier : IWindowSettingsApplier
    {
        public void Apply(AppSettings settings)
        {
            var window = Application.Current.MainWindow;
            if (window == null) return;

            if (settings.IsFullScreen)
            {
                ApplyFullScreen(window);
                return;
            }

            ApplyWindowedMode(window);
            ApplyResolution(window, settings.Resolution);
        }

        private static void ApplyFullScreen(Window window)
        {
            window.WindowStyle = WindowStyle.None;
            window.WindowState = WindowState.Maximized;
        }

        private static void ApplyWindowedMode(Window window)
        {
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Normal;
        }

        private static void ApplyResolution(Window window, string resolution)
        {
            if (!TryParseResolution(resolution, out double width, out double height))
            {
                return;
            }

            window.Width = width;
            window.Height = height;
        }

        private static bool TryParseResolution(string resolution, out double width, out double height)
        {
            width = 0;
            height = 0;

            if (string.IsNullOrWhiteSpace(resolution))
            {
                return false;
            }

            var parts = resolution.Split('×');
            if (parts.Length < 2)
            {
                return false;
            }

            return double.TryParse(parts[0].Trim(), out width)
                && double.TryParse(parts[1].Trim().Split(' ')[0], out height);
        }
    }
}
