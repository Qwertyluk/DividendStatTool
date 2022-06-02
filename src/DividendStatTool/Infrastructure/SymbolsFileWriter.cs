using DividendStatTool.Infrastructure.Contracts;
using System.Collections.Generic;
using System.IO;

namespace DividendStatTool.Infrastructure
{
    internal class SymbolsFileWriter : ISymbolsFileWriter
    {
        public void WriteSymbols(string path, IEnumerable<string> symbols)
        {
            File.WriteAllLines(path, symbols);
        }
    }
}
