using CHECKERS.Helpers;
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
            var formatted = MoveFormatter.Format(_history.GetHistory());
            foreach (var entry in formatted)
                Entries.Add(entry);
        }
        public void Clear()
        {
            _history.Clear();
            Entries.Clear();
        }
    }
}