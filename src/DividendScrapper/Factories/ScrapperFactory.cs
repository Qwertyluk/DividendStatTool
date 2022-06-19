using DividendScrapper.Contracts;
using DividendScrapper.Factories;
using DividendScrapper.Factories.Contracts;

namespace DividendScrapper
{
    public class ScrapperFactory : IScrapperFactory
    {
        private readonly IHtmlDocProvider _htmlDocProvider;
        private readonly SingleScrappersFactory _singleScrappersFactory;

        public ScrapperFactory() : this(new HtmlDocProvider()) { }

        internal ScrapperFactory(IHtmlDocProvider htmlDocProvider)
            : this(htmlDocProvider, new SingleScrappersFactory(new MeasureTextScrapperFactory())) { }

        internal ScrapperFactory(IHtmlDocProvider htmlDocProvider, IMeasureTextScrapperFactory measureTextScrapperFactory)
            : this(htmlDocProvider, new SingleScrappersFactory(measureTextScrapperFactory)) { }

        internal ScrapperFactory(IHtmlDocProvider htmlDocProvider, SingleScrappersFactory singleScrappersFactory)
        {
            _htmlDocProvider = htmlDocProvider;
            _singleScrappersFactory = singleScrappersFactory;
        }

        public IScrapper GetScrapper(string companySymbol)
        {
            return new Scrapper(_singleScrappersFactory.GetScrappers(_htmlDocProvider.GetHtmlDocument(companySymbol)));
        }
    }
}
