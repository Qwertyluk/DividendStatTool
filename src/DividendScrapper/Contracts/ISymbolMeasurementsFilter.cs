using DividendStatToolLibrary.Data;

namespace DividendScrapper.Contracts
{
    public interface ISymbolMeasurementsFilter
    {
        IEnumerable<SymbolMeasurement> Filter(IEnumerable<SymbolMeasurement> symbolMeasuremenets);
    }
}
