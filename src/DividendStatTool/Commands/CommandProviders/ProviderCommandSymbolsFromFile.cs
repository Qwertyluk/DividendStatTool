using Common.Contracts;
using DividendStatTool.Commands.Factories.Contracts;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.Factories
{
    internal class ProviderCommandSymbolsFromFile : IProviderCommandSymbolsFromFile
    {
        private readonly IOpenFileDialogService openFileDialogService;
        private readonly IFileReader fileReader;
        private readonly IFilterStringCollection filter;

        public ProviderCommandSymbolsFromFile(
            IOpenFileDialogService openFileDialogService,
            IFileReader fileReader,
            IFilterStringCollection filter)
        {
            this.openFileDialogService = openFileDialogService;
            this.fileReader = fileReader;
            this.filter = filter;
        }

        public ICommand GetCommandSymbolsFromFile(IMainWindowViewModel viewModel)
        {
            return new CommandSymbolsFromFile(viewModel, openFileDialogService, fileReader, filter);
        }
    }
}
