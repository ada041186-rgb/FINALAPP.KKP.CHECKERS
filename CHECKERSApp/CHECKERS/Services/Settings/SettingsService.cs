using CHECKERS.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace CHECKERS.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private static readonly string SettingsDir =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        private static readonly string SettingsPath =
            Path.Combine(SettingsDir, "settings.json");

        public AppSettings Load()
        {
            try
            {
                if (!File.Exists(SettingsPath)) return new AppSettings();
                var json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            catch { return new AppSettings(); }
        }

        public void Save(AppSettings settings)
        {
            try
            {
                var json = JsonSerializer.Serialize(settings,
                    new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsPath, json);
            }
            catch { }
        }
    }
}