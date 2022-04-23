using DividendStatToolLibrary.Contracts;

namespace DividendStatToolLibrary
{
    public class FilterSymbolsFromFile : IFilterStringCollection
    {
        public IEnumerable<string> Filter(IEnumerable<string> stringCollection)
        {
            return stringCollection.Where(x => !string.IsNullOrEmpty(x)).Distinct();
        }
    }
}
