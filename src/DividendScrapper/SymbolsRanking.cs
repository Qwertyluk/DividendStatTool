using DividendScrapper.Contracts;
using DividendStatToolLibrary.Data;

namespace DividendStatToolLibrary
{
    public class SymbolsRanking : ISymbolsRanking
    {
        public SortedList<int, SymbolMeasurement> AssignRanks(IEnumerable<SymbolMeasurement> measurements)
        {
            if (!measurements.Any())
            {
                return new SortedList<int, SymbolMeasurement>();
            }

            Dictionary<SymbolMeasurement, int> scoreBoard = AssingPoints(measurements);
            var sortedScoreBoard = scoreBoard.OrderByDescending(x => x.Value);

            return AssignRanks(sortedScoreBoard);
        }

        private Dictionary<SymbolMeasurement, int> AssingPoints(IEnumerable<SymbolMeasurement> measurements)
        {
            Dictionary<SymbolMeasurement, int> dict = InitScoreBoard(measurements);

            for (int i = 0; i < measurements.ElementAt(0).NumberOfMeasurements; i++)
            {
                var sortedSymbols = measurements.OrderBy(m => m.Measuremenets[i]);

                for (int j = 0; j < sortedSymbols.Count(); j++)
                {
                    dict[sortedSymbols.ElementAt(i)] += j;
                }
            }

            return dict;
        }

        private Dictionary<SymbolMeasurement, int> InitScoreBoard(IEnumerable<SymbolMeasurement> measurements)
        {
            Dictionary<SymbolMeasurement, int> dict = new Dictionary<SymbolMeasurement, int>();

            foreach (SymbolMeasurement measurement in measurements)
            {
                dict.Add(measurement, 0);
            }

            return dict;
        }

        private SortedList<int, SymbolMeasurement> AssignRanks(IEnumerable<KeyValuePair<SymbolMeasurement, int>> scoreBoard)
        {
            var ranks = new SortedList<int, SymbolMeasurement>();
            for (int i = 0; i < scoreBoard.Count(); i++)
            {
                ranks.Add(i, scoreBoard.ElementAt(i).Key);
            }

            return ranks;
        }
    }
}
