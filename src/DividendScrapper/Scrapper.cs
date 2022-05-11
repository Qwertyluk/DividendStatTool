using DividendScrapper.Contracts;
using DividendScrapper.Data;
using HtmlAgilityPack;

namespace DividendScrapper
{
    public class Scrapper
    {
        private readonly HtmlDocument htmlDoc;
        private readonly ISingleMeasureScrapper[] scrappers;

        internal Scrapper(HtmlDocument htmlDoc, ISingleMeasureScrapper[] scrappers)
        {
            this.htmlDoc = htmlDoc;
            this.scrappers = scrappers;
        }

        public Measurement[] Scrap()
        {
            Measurement[] measurements = new Measurement[scrappers.Length];
            for (int i = 0; i < scrappers.Length; i++)
            {
                measurements[i] = scrappers[i].ScrapMeasure(htmlDoc);
            }

            return measurements;
        }
    }
}
