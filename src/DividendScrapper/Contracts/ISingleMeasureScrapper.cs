using DividendScrapper.Data;
using HtmlAgilityPack;

namespace DividendScrapper.Contracts
{
    internal interface ISingleMeasureScrapper
    {
        Measurement ScrapMeasure(HtmlDocument htmlDoc);
    }
}
