using Common.Extensions;
using DividendScrapper.Contracts;

namespace DividendScrapper
{
    internal class SingleScrappersFactory
    {
        private readonly static string scrapInvalidText = "-";
        private readonly Func<string, bool> isValid = s => s != scrapInvalidText;

        public ISingleMeasureScrapper[] GetScrappers()
        {
            return new ISingleMeasureScrapper[6]
            {
                new SingleMeasureScrapper(
                    new MeasureTextScrapper("Debt/Eq"),
                    s => Convert.ToDouble(s),
                    isValid,
                    CompanyMeasurementNames.DebtPerEquity),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper("Payout"),
                    s => s.PercentageToDouble(),
                    isValid,
                    CompanyMeasurementNames.DividendPayoutRatio),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper("Dividend %"),
                    s => s.PercentageToDouble(),
                    isValid,
                    CompanyMeasurementNames.DividendYield),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper("Market Cap"),
                    s => s.BillionAnnotationToDouble(),
                    isValid,
                    CompanyMeasurementNames.MarketCapitalization),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper("P/E"),
                    s => Convert.ToDouble(s),
                    isValid,
                    CompanyMeasurementNames.PricePerEarnings),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper("ROE"),
                    s => s.PercentageToDouble(),
                    isValid,
                    CompanyMeasurementNames.ReturnOnEquity),
            };
        }
    }
}
