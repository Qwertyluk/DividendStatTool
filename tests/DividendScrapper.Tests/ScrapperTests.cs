using DividendScrapper.Contracts;
using DividendScrapper.Data;
using FluentAssertions;
using FluentAssertions.Execution;
using HtmlAgilityPack;
using Moq;
using TestsCommon;
using Xunit;

namespace DividendScrapper.Tests
{
    public class ScrapperTests
    {
        [Theory]
        [Trait("Category", "Integration")]
        [InlineData("finvizAAPL.html", 1.78, 0.142, 0.0058, 2537_350_000_000, 25.57, 1.529)]
        [InlineData("finvizTSLA.html", 0.14, 0, 0, 803_450_000_000, 108.14, 0.289)]
        public void Scrap_FromFile_ShouldScrap(
            string fileName,
            double expectedDPE,
            double expectedDPR,
            double expectedDY,
            double expectedMC,
            double expectedPPE,
            double expectedROE)
        {
            // Arrange
            HtmlDocument htmlDoc = GetHtmlDocFromFile(TestData.GetFullResourceDirectory(fileName));
            string companySymbol = "testSymbol";
            Mock<IHtmlDocProvider> mockHtmlDocProvider = new Mock<IHtmlDocProvider>();
            mockHtmlDocProvider.Setup(m => m.GetHtmlDocument(companySymbol)).Returns(htmlDoc);
            ScrapperFactory scrapperFactory = new ScrapperFactory(mockHtmlDocProvider.Object);
            Scrapper scrapper = scrapperFactory.GetScrapper(companySymbol);

            // Act
            Measurement[] measures = scrapper.Scrap();

            // Assert
            using (new AssertionScope())
            {
                measures.Should().ContainEquivalentOf(new Measurement("Debt Per Equity", expectedDPE));
                measures.Should().ContainEquivalentOf(new Measurement("Dividend Payout Ratio", expectedDPR));
                measures.Should().ContainEquivalentOf(new Measurement("Dividend Yield", expectedDY));
                measures.Should().ContainEquivalentOf(new Measurement("Market Capitalization", expectedMC));
                measures.Should().ContainEquivalentOf(new Measurement("Price Per Earnings", expectedPPE));
                measures.Should().ContainEquivalentOf(new Measurement("Return On Equity", expectedROE));
            }
        }

        [Theory]
        [Trait("Category", "Integration")]
        [InlineData("AAPL")]
        [InlineData("TSLA")]
        public void Scrap_FromWeb_ShouldScrap(string companySymbol)
        {
            // Arrange
            ScrapperFactory scrapperFactory = new ScrapperFactory();
            Scrapper scrapper = scrapperFactory.GetScrapper(companySymbol);

            // Act
            Measurement[] measures = scrapper.Scrap();

            // Assert
            using (new AssertionScope())
            {
                measures.Should().Contain(m => m.Name == CompanyMeasurementNames.DebtPerEquity);
                measures.Should().Contain(m => m.Name == CompanyMeasurementNames.DividendPayoutRatio);
                measures.Should().Contain(m => m.Name == CompanyMeasurementNames.DividendYield);
                measures.Should().Contain(m => m.Name == CompanyMeasurementNames.MarketCapitalization);
                measures.Should().Contain(m => m.Name == CompanyMeasurementNames.PricePerEarnings);
                measures.Should().Contain(m => m.Name == CompanyMeasurementNames.ReturnOnEquity);
            }
        }

        private HtmlDocument GetHtmlDocFromFile(string filePath)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filePath);
            return doc;
        }
    }
}
