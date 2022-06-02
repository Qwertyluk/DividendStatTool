using CommonUI.Contracts;
using DividendStatTool.Infrastructure;
using DividendStatTool.Infrastructure.Contracts;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;

namespace DividendStatTool.Commands
{
    internal class CommandSymbolsWriteToFile : CommandExecutableWhenSymbolsExist
    {
        private readonly IOpenFileDialogService openFileDialog;
        private readonly ISymbolsFileWriter writer;
        private readonly IMessageHandler messageHandler;

        public CommandSymbolsWriteToFile(
            IMainWindowViewModel viewModel,
            IOpenFileDialogService openFileDialog,
            ISymbolsFileWriter writer,
            IMessageHandler messageHandler) : base(viewModel)
        {
            this.openFileDialog = openFileDialog;
            this.writer = writer;
            this.messageHandler = messageHandler;
        }

        #region ICommand

        public override void Execute(object? parameter)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                writer.WriteSymbols(openFileDialog.FileName, viewModel.Symbols);
                messageHandler.HandleInfo(StringLiterals.Success, StringLiterals.SymbolsSavedToFileMessage);
            }
        }

        #endregion
    }
}
