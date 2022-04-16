using DividendCalculation.Services.Contracts;
using DividendStatToolLibrary.Contracts;
using System.IO;

namespace DividendStatToolLibrary
{
    public class SymbolsProviderFromFile : ISymbolsProvider
    {
        private readonly IOpenFileDialogService openFileDialog;

        public SymbolsProviderFromFile(IOpenFileDialogService openFileDialog)
        {
            this.openFileDialog = openFileDialog;
        }

        public bool Succeeded { get; private set; }

        public List<string> Symbols { get; private set; } = new List<string>();

        public void GetSymbols()
        {
            if (openFileDialog.ShowDialog() == true)
            {
                Symbols = File.ReadAllLines(openFileDialog.FileName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                Succeeded = true;
            }
            else
            {
                Succeeded = false;
            }
        }
    }
}
