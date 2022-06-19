using DividendScrapper.Contracts;
using DividendScrapper.Factories.Contracts;
using HtmlAgilityPack;

namespace DividendScrapper.Factories
{
    internal class MeasureTextScrapperFactory : IMeasureTextScrapperFactory
    {
        public IMeasureTextScrapper CreateMeasureTextScrapper(HtmlDocument htmlDoc, string symbol)
        {
            return new MeasureTextScrapper(htmlDoc, symbol);
        }
    }
}
