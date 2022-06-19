using DividendStatToolLibrary.Data;

namespace DividendScrapper.Contracts
{
    public interface ISymbolsScrapper
    {
        List<string> FailedSymbols { get; }
        Action<int>? CallBackProgress { get; set; }
        Func<bool>? CallBackCancel { get; set; }
        IEnumerable<SymbolMeasurement> GetSymbolsMeasurements(IEnumerable<string> symbolNames);
    }
}
