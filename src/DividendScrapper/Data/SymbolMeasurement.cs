using DividendScrapper.Data;

namespace DividendStatToolLibrary.Data
{
    public class SymbolMeasurement
    {
        public string SymbolName { get; }

        public Measurement[] Measuremenets { get; }
        public int NumberOfMeasurements => Measuremenets.Length;

        public SymbolMeasurement(string symbolName, Measurement[] measuremenets)
        {
            SymbolName = symbolName;
            Measuremenets = measuremenets;
        }

        public override string ToString()
        {
            return SymbolName;
        }
    }
}
