using Common.Contracts;
using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.Factories
{
    internal class ProviderCommandSymbolsFromFile : IMainWindowCommandProvider
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

        public ICommand GetCommand(IMainWindowViewModel viewModel)
        {
            return new CommandSymbolsFromFile(viewModel, openFileDialogService, fileReader, filter);
        }
    }
}
