using DividendStatToolLibrary.Data;

namespace DividendScrapper.Contracts
{
    public interface ISymbolsRanking
    {
        SortedList<int, SymbolMeasurement> AssignRanks(IEnumerable<SymbolMeasurement> measurements);
    }
}
