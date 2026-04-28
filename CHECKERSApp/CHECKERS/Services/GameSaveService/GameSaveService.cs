using CHECKERS.Models;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace CHECKERS.Services
{
    public class GameSaveService : IGameSaveService
    {
        private static readonly string SavePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "savegame.json");

        public void Save(GameSnapshot snapshot)
        {
            var json = JsonSerializer.Serialize(snapshot,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SavePath, json);
        }

        public GameSnapshot? Load()
        {
            if (!HasSave()) return null;
            var json = File.ReadAllText(SavePath);
            return JsonSerializer.Deserialize<GameSnapshot>(json);
        }

        public bool HasSave() => File.Exists(SavePath);

        public void Delete()
        {
            if (HasSave()) File.Delete(SavePath);
        }
    }
}