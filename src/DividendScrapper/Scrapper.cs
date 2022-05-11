using DividendScrapper.Contracts;
using DividendScrapper.Data;

namespace DividendScrapper
{
    public class Scrapper
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
    }
}
