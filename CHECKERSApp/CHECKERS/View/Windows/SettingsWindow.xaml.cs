using CHECKERS.ViewModels;
using System.Windows;

namespace CHECKERS.View.Windows
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(SettingsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.RequestClose += () => Close();
        }
    }
}