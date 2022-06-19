using Common.Extensions;
using DividendScrapper.Contracts;
using DividendScrapper.Enums;
using DividendScrapper.Factories;
using DividendScrapper.Factories.Contracts;
using HtmlAgilityPack;

namespace DividendScrapper
{
    internal class SingleScrappersFactory
    {
        private readonly static string scrapInvalidText = "-";
        private readonly Func<string, bool> isValid = s => s != scrapInvalidText;
        private readonly IMeasureTextScrapperFactory measureTextScrapperFactory;

        public SingleScrappersFactory(IMeasureTextScrapperFactory measureTextScrapperFactory)
        {
            this.measureTextScrapperFactory = measureTextScrapperFactory;
        }

        public ISingleMeasureScrapper[] GetScrappers(HtmlDocument htmlDoc)
        {
            return new ISingleMeasureScrapper[6]
            {
                new SingleMeasureScrapper(
                    measureTextScrapperFactory.CreateMeasureTextScrapper(htmlDoc, "Debt/Eq"),
                    s => Convert.ToDouble(s),
                    isValid,
                    Factor.DebtPerEquity,
                    new MeasurementFactory(
                        d => d <= 1,
                        (c, m) => c.Value.CompareTo(m?.Value) * -1)),
                new SingleMeasureScrapper(
                    measureTextScrapperFactory.CreateMeasureTextScrapper(htmlDoc, "Payout"),
                    s => s.PercentageToDouble(),
                    isValid,
                    Factor.DividendPayoutRatio,
                    new MeasurementFactory(
                        d => d >= 0.2 && d <= 0.6,
                        (c, m) => c.Value.CompareTo(m?.Value))),
                new SingleMeasureScrapper(
                    measureTextScrapperFactory.CreateMeasureTextScrapper(htmlDoc, "Dividend %"),
                    s => s.PercentageToDouble(),
                    isValid,
                    Factor.DividendYield,
                    new MeasurementFactory(
                        d => d >= 0.02,
                        (c, m) => c.Value.CompareTo(m?.Value))),
                new SingleMeasureScrapper(
                    measureTextScrapperFactory.CreateMeasureTextScrapper(htmlDoc, "Market Cap"),
                    s => s.NumberWithAbbreviationToDouble(),
                    isValid,
                    Factor.MarketCapitalization,
                    new MeasurementFactory(
                        d => d >= 10_000_000_000,
                        (c, m) => c.Value.CompareTo(m?.Value))),
                new SingleMeasureScrapper(
                    measureTextScrapperFactory.CreateMeasureTextScrapper(htmlDoc, "P/E"),
                    s => Convert.ToDouble(s),
                    isValid,
                    Factor.PricePerEarnings,
                    new MeasurementFactory(
                        d => d <= 20,
                        (c, m) => c.Value.CompareTo(m?.Value) * -1)),
                new SingleMeasureScrapper(
                    measureTextScrapperFactory.CreateMeasureTextScrapper(htmlDoc, "ROE"),
                    s => s.PercentageToDouble(),
                    isValid,
                    Factor.ReturnOnEquity,
                    new MeasurementFactory(
                        d => d >= 0.1,
                        (c, m) => c.Value.CompareTo(m?.Value))),
            };
        }
    }
}
