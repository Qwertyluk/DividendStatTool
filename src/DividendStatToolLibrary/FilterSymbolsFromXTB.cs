using DividendStatToolLibrary.Contracts;

namespace DividendStatToolLibrary
{
    public class FilterSymbolsFromXTB : IFilterStringCollection
    {
        public IEnumerable<string> Filter(IEnumerable<string> stringCollection)
        {
            return stringCollection.Select(x => x.Substring(0, x.IndexOf('.')));
        }
    }
}
