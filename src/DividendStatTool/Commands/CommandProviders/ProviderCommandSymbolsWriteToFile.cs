using CommonUI.Contracts;
using DividendStatTool.Commands.CommandProviders.Contracts;
using DividendStatTool.Infrastructure.Contracts;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;
using System.Windows.Input;

namespace DividendStatTool.Commands.CommandProviders
{
    internal class ProviderCommandSymbolsWriteToFile : IMainWindowCommandProvider
    {
        private readonly IOpenFileDialogService openFileDialogService;
        private readonly ISymbolsFileWriter symbolsWriter;
        private readonly IMessageHandler messageHandler;

        public ProviderCommandSymbolsWriteToFile(
            IOpenFileDialogService openFileDialogService,
            ISymbolsFileWriter symbolsWriter,
            IMessageHandler messageHandler)
        {
            this.openFileDialogService = openFileDialogService;
            this.symbolsWriter = symbolsWriter;
            this.messageHandler = messageHandler;
        }

        public ICommand GetCommand(IMainWindowViewModel viewModel)
        {
            return new CommandSymbolsWriteToFile(viewModel, openFileDialogService, symbolsWriter, messageHandler);
        }
    }
}
