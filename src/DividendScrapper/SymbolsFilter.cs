using DividendScrapper.Contracts;
using DividendStatToolLibrary.Data;

namespace DividendScrapper
{
    public class SymbolsFilter : ISymbolMeasurementsFilter
    {
        public IEnumerable<SymbolMeasurement> Filter(IEnumerable<SymbolMeasurement> symbolMeasuremenets)
        {
            return symbolMeasuremenets.Where(m => m.Measurements.All(m => m.IsValid));
        }
    }
}
