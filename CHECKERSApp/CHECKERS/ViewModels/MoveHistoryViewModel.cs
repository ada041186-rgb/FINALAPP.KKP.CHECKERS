using CHECKERS.Models;
using CHECKERS.Services;
using CHECKERS.ViewModels.Base;
using System.Collections.ObjectModel;

namespace CHECKERS.ViewModels
{
    public class MoveHistoryViewModel : ViewModel
    {
        private readonly IMoveHistoryService _history;

        public ObservableCollection<string> Entries { get; } = new();

        public MoveHistoryViewModel(IMoveHistoryService history)
        {
            _history = history;
        }

        public void Refresh()
        {
            Entries.Clear();
            var all = _history.GetHistory();
            for (int i = 0; i < all.Count; i++)
            {
                var m = all[i];
                string side = (i % 2 == 0) ? "Білі" : "Чорні";
                string capture = m.Captured != null ? " (захоплення)" : "";
                Entries.Add($"{i + 1}. {side}: ({m.From.Row},{m.From.Column}) → ({m.To.Row},{m.To.Column}){capture}");
            }
        }

        public void Clear()
        {
            _history.Clear();
            Entries.Clear();
        }
    }
}