using DividendScrapper.Contracts;
using DividendScrapper.Data;
using DividendScrapper.Enums;

namespace DividendScrapper
{
    public class Scrapper : IScrapper
    {
        private readonly ISingleMeasureScrapper[] scrappers;

        internal Scrapper(ISingleMeasureScrapper[] scrappers)
        {
            this.scrappers = scrappers;
        }

        public Measurement[] Scrap()
        {
            Measurement[] measurements = new Measurement[scrappers.Length];
            for (int i = 0; i < scrappers.Length; i++)
            {
                measurements[i] = scrappers[i].ScrapMeasure();
            }

            return measurements;
        }

        public Measurement Scrap(Factor factor)
        {
            var scrapper = scrappers.First(s => s.Factor == factor);
            return scrapper.ScrapMeasure();
        }
    }
}
