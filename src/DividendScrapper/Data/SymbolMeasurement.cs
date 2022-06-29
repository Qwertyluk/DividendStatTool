using DividendScrapper.Data;

namespace DividendStatToolLibrary.Data
{
    public class SymbolMeasurement
    {
        public string SymbolName { get; }

        public Measurement[] Measurements { get; }
        public int NumberOfMeasurements => Measurements.Length;

        public SymbolMeasurement(string symbolName, Measurement[] measuremenets)
        {
            SymbolName = symbolName;
            Measurements = measuremenets;
        }

        public override string ToString()
        {
            return SymbolName;
        }
    }
}
