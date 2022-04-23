namespace DividendStatToolLibrary.Contracts
{
    public interface IFilterStringCollection
    {
        IEnumerable<string> Filter(IEnumerable<string> stringCollection);
    }
}
