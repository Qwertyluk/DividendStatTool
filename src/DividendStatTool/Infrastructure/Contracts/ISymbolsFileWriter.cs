using System.Collections.Generic;

namespace DividendStatTool.Infrastructure.Contracts
{
    internal interface ISymbolsFileWriter
    {
        void WriteSymbols(string path, IEnumerable<string> symbols);
    }
}
