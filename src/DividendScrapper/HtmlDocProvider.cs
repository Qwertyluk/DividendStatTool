using Common.Extensions;
using DividendScrapper.Contracts;
using HtmlAgilityPack;

namespace DividendScrapper
{
    internal class HtmlDocProvider : IHtmlDocProvider
    {
        private readonly string baseHtml = @"https://finviz.com/quote.ashx";

        public HtmlDocument GetHtmlDocument(string symbol)
        {
            Uri uriWithParameter = new Uri(baseHtml).AddParameter("t", symbol);
            return new HtmlWeb().Load(uriWithParameter.AbsoluteUri);
        }
    }
}
