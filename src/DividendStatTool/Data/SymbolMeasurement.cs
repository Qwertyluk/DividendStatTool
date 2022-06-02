using DividendScrapper.Data;

namespace DividendStatTool.Data
{
    internal class SymbolMeasurement
    {
        public string SymbolName { get; }
        public Measurement[] Measuremenets { get; }

        public SymbolMeasurement(string symbolName, Measurement[] measuremenets)
        {
            SymbolName = symbolName;
            Measuremenets = measuremenets;
        }
    }
}
