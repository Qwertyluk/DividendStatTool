﻿using DividendScrapper.Contracts;
using DividendScrapper.Factories.Contracts;

namespace DividendScrapper
{
    public class ScrapperFactory : IScrapperFactory
    {
        private readonly IHtmlDocProvider _htmlDocProvider;
        private readonly SingleScrappersFactory _singleScrappersFactory;

        public ScrapperFactory() : this(new HtmlDocProvider()) { }

        internal ScrapperFactory(IHtmlDocProvider htmlDocProvider) : this(htmlDocProvider, new SingleScrappersFactory()) { }

        internal ScrapperFactory(IHtmlDocProvider htmlDocProvider, SingleScrappersFactory singleScrappersFactory)
        {
            _htmlDocProvider = htmlDocProvider;
            _singleScrappersFactory = singleScrappersFactory;
        }

        public Scrapper GetScrapper(string companySymbol)
        {
            return new Scrapper(_singleScrappersFactory.GetScrappers(_htmlDocProvider.GetHtmlDocument(companySymbol)));
        }
    }
}
