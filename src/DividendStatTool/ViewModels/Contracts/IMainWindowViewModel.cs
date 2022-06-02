using System.ComponentModel;

namespace DividendStatTool.ViewModels.Contracts
{
    internal interface IMainWindowViewModel
    {
        int Progress { set; }
        BindingList<string> Symbols { get; }
    }
}
