using Common.Extensions;
using DividendScrapper.Contracts;
using HtmlAgilityPack;

namespace DividendScrapper
{
    internal class SingleScrappersFactory
    {
        private readonly static string scrapInvalidText = "-";
        private readonly Func<string, bool> isValid = s => s != scrapInvalidText;

        public ISingleMeasureScrapper[] GetScrappers(HtmlDocument htmlDoc)
        {
            return new ISingleMeasureScrapper[6]
            {
                new SingleMeasureScrapper(
                    new MeasureTextScrapper(htmlDoc, "Debt/Eq"),
                    s => Convert.ToDouble(s),
                    isValid,
                    CompanyMeasurementNames.DebtPerEquity),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper(htmlDoc, "Payout"),
                    s => s.PercentageToDouble(),
                    isValid,
                    CompanyMeasurementNames.DividendPayoutRatio),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper(htmlDoc, "Dividend %"),
                    s => s.PercentageToDouble(),
                    isValid,
                    CompanyMeasurementNames.DividendYield),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper(htmlDoc, "Market Cap"),
                    s => s.BillionAnnotationToDouble(),
                    isValid,
                    CompanyMeasurementNames.MarketCapitalization),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper(htmlDoc, "P/E"),
                    s => Convert.ToDouble(s),
                    isValid,
                    CompanyMeasurementNames.PricePerEarnings),
                new SingleMeasureScrapper(
                    new MeasureTextScrapper(htmlDoc, "ROE"),
                    s => s.PercentageToDouble(),
                    isValid,
                    CompanyMeasurementNames.ReturnOnEquity),
            };
        }
    }
}
