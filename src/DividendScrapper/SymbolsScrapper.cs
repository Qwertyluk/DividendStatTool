using DividendScrapper.Contracts;
using DividendScrapper.Data;
using DividendScrapper.Exceptions;
using DividendScrapper.Factories.Contracts;
using DividendStatToolLibrary.Data;

namespace DividendScrapper
{
    public class SymbolsScrapper : ISymbolsScrapper
    {
        internal IScrapperFactory ScrapperFactory { get; set; } = new ScrapperFactory();

        public List<string> FailedSymbols { get; } = new List<string>();
        public Action<int>? CallBackProgress { get; set; }
        public Func<bool>? CallBackCancel { get; set; }

        public IEnumerable<SymbolMeasurement> GetSymbolsMeasurements(IEnumerable<string> symbolNames)
        {
            List<SymbolMeasurement> symbolMeasures = new List<SymbolMeasurement>();
            FailedSymbols.Clear();
            int i = 0;
            int symbolNamesCount = symbolNames.Count();

            foreach (var symbolName in symbolNames)
            {
                if (CallBackCancel?.Invoke() == true)
                {
                    return symbolMeasures;
                }

                IScrapper scrapper = ScrapperFactory.GetScrapper(symbolName);
                try
                {
                    Measurement[] results = scrapper.Scrap();
                    symbolMeasures.Add(new SymbolMeasurement(symbolName, results));
                }
                catch (TextScrapException)
                {
                    FailedSymbols.Add(symbolName);
                }

                CallBackProgress?.Invoke((int)(100.0 * ++i / symbolNamesCount));
            }

            return symbolMeasures;
        }
    }
}
