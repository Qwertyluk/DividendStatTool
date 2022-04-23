using Common.Contracts;
using DividendStatTool.Services.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using System.Collections.Generic;

namespace DividendStatTool.Commands
{
    internal class CommandSymbolsFromFile : CommandSymbols
    {
        private readonly IOpenFileDialogService openFileDialog;
        private readonly IFileReader fileReader;
        private readonly IFilterStringCollection filter;


        public CommandSymbolsFromFile(
            IMainWindowViewModel windowViewModel,
            IOpenFileDialogService openFileDialog,
            IFileReader fileReader,
            IFilterStringCollection filter) : base(windowViewModel)
        {
            this.openFileDialog = openFileDialog;
            this.fileReader = fileReader;
            this.filter = filter;
        }

        public override void Execute(object? parameter)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                IEnumerable<string> readSymbols = fileReader.ReadAllLines(openFileDialog.FileName);
                IEnumerable<string> filteredSymbols = filter.Filter(readSymbols);
                SetViewModelSymbols(filteredSymbols);
            }
        }
    }
}
