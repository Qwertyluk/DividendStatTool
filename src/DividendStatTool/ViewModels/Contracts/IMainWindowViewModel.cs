using System.ComponentModel;
using System.Windows.Input;

namespace DividendStatTool.ViewModels.Contracts
{
    internal interface IMainWindowViewModel
    {
        ICommand ButtonFetchFromFileCommand { get; }

        BindingList<string> Symbols { get; }
    }
}
