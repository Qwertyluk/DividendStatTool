using DividendScrapper.Contracts;
using HtmlAgilityPack;

namespace DividendScrapper.Factories.Contracts
{
    internal interface IMeasureTextScrapperFactory
    {
        IMeasureTextScrapper CreateMeasureTextScrapper(HtmlDocument htmlDoc, string symbol);
    }
}
