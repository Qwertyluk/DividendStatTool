using Common.Extensions;
using DividendScrapper.Contracts;
using HtmlAgilityPack;

namespace DividendScrapper
{
    internal class HtmlDocProvider : IHtmlDocProvider
    {
        public HtmlDocument GetHtmlDocument(string symbol)
        {
            Uri uriWithParameter = new Uri(StringLiterals.BaseHtml).AddParameter("t", symbol);
            return new HtmlWeb().Load(uriWithParameter.AbsoluteUri);
        }
    }
}
