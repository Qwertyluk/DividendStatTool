using HtmlAgilityPack;

namespace DividendScrapper.Contracts
{
    internal interface IHtmlDocProvider
    {
        HtmlDocument GetHtmlDocument(string symbol);
    }
}
