namespace DividendStatToolLibrary.Contracts
{
    internal interface IFileReader
    {
        IEnumerable<string> ReadLines(string filePath);
    }
}
