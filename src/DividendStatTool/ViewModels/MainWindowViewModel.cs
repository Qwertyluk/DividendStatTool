using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.ViewModels.Contracts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DividendStatTool.ViewModels
{
    internal class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        public ICommand ButtonReadFromFileCommand { get; }
        public ICommand ButtonSaveToFileCommand { get; }
        public ICommand ButtonFetchFromXTBCommand { get; }
        public ICommand ButtonRunCalculationsCommand { get; }
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> Symbols { get; } = new BindingList<string>();

        private int progress;

        public MainWindowViewModel(
            IMainWindowCommandProvider commandSymbolsFromFileProvider,
            IMainWindowCommandProvider commandSaveToFileProvider,
            IMainWindowCommandProvider commandSymbolsFromXTBProvider,
            IMainWindowCommandProvider commandRunCalculationsProvider)
        {
            Progress = 0;
            ButtonReadFromFileCommand = commandSymbolsFromFileProvider.GetCommand(this);
            ButtonSaveToFileCommand = commandSaveToFileProvider.GetCommand(this);
            ButtonFetchFromXTBCommand = commandSymbolsFromXTBProvider.GetCommand(this);
            ButtonRunCalculationsCommand = commandRunCalculationsProvider.GetCommand(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
