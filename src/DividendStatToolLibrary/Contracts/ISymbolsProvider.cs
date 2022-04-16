namespace DividendStatToolLibrary.Contracts
{
    public interface ISymbolsProvider
    {
        public bool Succeeded { get; }
        public List<string> Symbols { get; }
        public void GetSymbols();
    }
}
