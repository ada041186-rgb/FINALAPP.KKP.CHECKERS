using CHECKERS.Models;
using System.Windows;

namespace CHECKERS.Services
{
    public class WindowSettingsApplier : IWindowSettingsApplier
    {
        public void Apply(AppSettings settings)
        {
            var w = Application.Current.MainWindow;
            if (w == null) return;

            if (settings.IsFullScreen)
            {
                w.WindowStyle = WindowStyle.None;
                w.WindowState = WindowState.Maximized;
                return;
            }

            w.WindowStyle = WindowStyle.SingleBorderWindow;
            w.WindowState = WindowState.Normal;

            var parts = settings.Resolution.Split('×');
            if (parts.Length >= 2
                && double.TryParse(parts[0].Trim(), out double width)
                && double.TryParse(parts[1].Trim().Split(' ')[0], out double height))
            {
                w.Width = width;
                w.Height = height;
            }
        }
    }
}