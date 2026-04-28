using CHECKERS.Models;

namespace CHECKERS.Services
{
    public interface IGameSaveService
    {
        void Save(GameSnapshot snapshot);
        GameSnapshot? Load();
        bool HasSave();
        void Delete();
    }
}