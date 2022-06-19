using DividendScrapper.Contracts;
using DividendScrapper.Data;
using DividendScrapper.Enums;
using DividendScrapper.Exceptions;
using DividendScrapper.Factories;
using FluentAssertions;
using FluentAssertions.Execution;
using HtmlAgilityPack;
using Moq;
using System;
using System.IO;
using System.Text;
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
        [InlineData("finvizGEO.html", 2.89, 0, 0, 863_100_000, 13.04, 0.065)]
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
            IScrapper scrapper = scrapperFactory.GetScrapper(companySymbol);

            // Act
            Measurement[] measures = scrapper.Scrap();

            // Assert
            measures.Should().Contain(m => m.Factor == Factor.DebtPerEquity && m.Value == expectedDPE);
            measures.Should().Contain(m => m.Factor == Factor.DividendPayoutRatio && m.Value == expectedDPR);
            measures.Should().Contain(m => m.Factor == Factor.DividendYield && m.Value == expectedDY);
            measures.Should().Contain(m => m.Factor == Factor.MarketCapitalization && m.Value == expectedMC);
            measures.Should().Contain(m => m.Factor == Factor.PricePerEarnings && m.Value == expectedPPE);
            measures.Should().Contain(m => m.Factor == Factor.ReturnOnEquity && m.Value == expectedROE);
        }

        [Theory]
        [Trait("Category", "Integration")]
        [InlineData("AAPL")]
        [InlineData("TSLA")]
        public void Scrap_FromWeb_ShouldScrap(string companySymbol)
        {
            // Arrange
            ScrapperFactory scrapperFactory = new ScrapperFactory();
            IScrapper scrapper = scrapperFactory.GetScrapper(companySymbol);

            // Act
            Measurement[] measures = scrapper.Scrap();

            // Assert
            using (new AssertionScope())
            {
                measures.Should().Contain(m => m.Factor == Factor.DebtPerEquity);
                measures.Should().Contain(m => m.Factor == Factor.DividendPayoutRatio);
                measures.Should().Contain(m => m.Factor == Factor.DividendYield);
                measures.Should().Contain(m => m.Factor == Factor.MarketCapitalization);
                measures.Should().Contain(m => m.Factor == Factor.PricePerEarnings);
                measures.Should().Contain(m => m.Factor == Factor.ReturnOnEquity);
            }
        }

        [Fact]
        public void Scrap_FromWeb_ShouldThrowException_WhenSymbolDoesntExist()
        {
            // Arrange
            ScrapperFactory scrapperFactory = new ScrapperFactory();
            IScrapper scrapper = scrapperFactory.GetScrapper("INVALID_SYMBOL");

            // Act
            Action act = () => scrapper.Scrap();

            // Assert
            act.Should().Throw<TextScrapException>().WithMessage("Can't find node.");
        }

        [Theory]
        [InlineData(Factor.DebtPerEquity, "0", true)]
        [InlineData(Factor.DebtPerEquity, "1", true)]
        [InlineData(Factor.DebtPerEquity, "1.1", false)]
        [InlineData(Factor.DividendPayoutRatio, "20%", true)]
        [InlineData(Factor.DividendPayoutRatio, "21%", true)]
        [InlineData(Factor.DividendPayoutRatio, "60%", true)]
        [InlineData(Factor.DividendPayoutRatio, "19%", false)]
        [InlineData(Factor.DividendPayoutRatio, "61%", false)]
        [InlineData(Factor.DividendYield, "2%", true)]
        [InlineData(Factor.DividendYield, "2.01%", true)]
        [InlineData(Factor.DividendYield, "1.99%", false)]
        [InlineData(Factor.MarketCapitalization, "10B", true)]
        [InlineData(Factor.MarketCapitalization, "10.01B", true)]
        [InlineData(Factor.MarketCapitalization, "9.99B", false)]
        [InlineData(Factor.PricePerEarnings, "20", true)]
        [InlineData(Factor.PricePerEarnings, "19.99", true)]
        [InlineData(Factor.PricePerEarnings, "20.01", false)]
        [InlineData(Factor.ReturnOnEquity, "10%", true)]
        [InlineData(Factor.ReturnOnEquity, "10.01%", true)]
        [InlineData(Factor.ReturnOnEquity, "9.99%", false)]
        public void Scrap_IsValid_ShouldReturnCorrectValue(Factor factor, string value, bool isValid)
        {
            // Arrange
            var symbol = "testSymbol";
            Mock<IHtmlDocProvider> mockHtmlDocProvider = new Mock<IHtmlDocProvider>();
            mockHtmlDocProvider.Setup(m => m.GetHtmlDocument(symbol))
                .Returns(new HtmlDocumentMock(factor, value));
            ScrapperFactory scrapperFactory = new ScrapperFactory(mockHtmlDocProvider.Object);
            var scrapper = scrapperFactory.GetScrapper(symbol);

            // Act
            var scrappedValue = scrapper.Scrap(factor);

            // Assert
            scrappedValue.IsValid.Should().Be(isValid);
        }

        [Theory]
        [InlineData(Factor.DebtPerEquity, "0", 0, 0)]
        [InlineData(Factor.DebtPerEquity, "1", 0, -1)]
        [InlineData(Factor.DebtPerEquity, "0", 1, 1)]
        [InlineData(Factor.DividendPayoutRatio, "30%", 0.3, 0)]
        [InlineData(Factor.DividendPayoutRatio, "30%", 0.4, -1)]
        [InlineData(Factor.DividendPayoutRatio, "30%", 0.2, 1)]
        [InlineData(Factor.DividendYield, "3%", 0.03, 0)]
        [InlineData(Factor.DividendYield, "3%", 0.04, -1)]
        [InlineData(Factor.DividendYield, "3%", 0.02, 1)]
        [InlineData(Factor.MarketCapitalization, "15B", 15_000_000_000, 0)]
        [InlineData(Factor.MarketCapitalization, "15B", 16_000_000_000, -1)]
        [InlineData(Factor.MarketCapitalization, "15B", 14_000_000_000, 1)]
        [InlineData(Factor.PricePerEarnings, "20", 20, 0)]
        [InlineData(Factor.PricePerEarnings, "20", 19, -1)]
        [InlineData(Factor.PricePerEarnings, "20", 21, 1)]
        [InlineData(Factor.ReturnOnEquity, "10%", 0.1, 0)]
        [InlineData(Factor.ReturnOnEquity, "10%", 0.11, -1)]
        [InlineData(Factor.ReturnOnEquity, "10%", 0.09, 1)]
        public void Scrap_CompareTo_ShouldReturnCorrectValue(Factor factor, string value, double valueToCompare, int expectedComparision)
        {
            // Arrange
            var symbol = "test";
            Mock<IHtmlDocProvider> mockHtmlDocProvider = new Mock<IHtmlDocProvider>();
            mockHtmlDocProvider.Setup(m => m.GetHtmlDocument(symbol))
                .Returns(new HtmlDocumentMock(factor, value));
            ScrapperFactory scrapperFactory = new ScrapperFactory(mockHtmlDocProvider.Object);
            var scrapper = scrapperFactory.GetScrapper(symbol);

            // Act
            var scrappedValue = scrapper.Scrap(factor);
            var comparisionResult = scrappedValue.CompareTo(
                new MeasurementFactory(null!, null!).CreateMeasurement(factor, valueToCompare));

            // Assert
            comparisionResult.Should().Be(expectedComparision);
        }

        private HtmlDocument GetHtmlDocFromFile(string filePath)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filePath);
            return doc;
        }

        private class HtmlDocumentMock : HtmlDocument
        {
            public HtmlDocumentMock(Factor factor, string value)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table>");
                switch (factor)
                {
                    case Factor.DebtPerEquity:
                        sb.Append(CreateTextData("Debt/Eq", value));
                        break;
                    case Factor.DividendPayoutRatio:
                        sb.Append(CreateTextData("Payout", value));
                        break;
                    case Factor.DividendYield:
                        sb.Append(CreateTextData("Dividend %", value));
                        break;
                    case Factor.MarketCapitalization:
                        sb.Append(CreateTextData("Market Cap", value));
                        break;
                    case Factor.PricePerEarnings:
                        sb.Append(CreateTextData("P/E", value));
                        break;
                    case Factor.ReturnOnEquity:
                        sb.Append(CreateTextData("ROE", value));
                        break;
                }
                sb.Append("</table>");
                var docContent = sb.ToString();
                Load(new MemoryStream(Encoding.UTF8.GetBytes(docContent)));
            }

            private string CreateTextData(string param, string value)
            {
                return "<tr><td width=\"7 % \" class=\"snapshot - td2 - cp\" " +
                    "align=\"left\" data-boxover=\"cssbody =[tooltip_short_bdy] " +
                    "cssheader =[tooltip_short_hdr] body =[Total Debt to Equity(mrq)] " +
                    $"offsetx =[10] offsety =[20] delay =[300]\">{param}</td><td width=\"" +
                    "8 % \" class=\"snapshot - td2\" align=\"left\"><b><span class=\" " +
                    $"is -red\">{value}</span></b></td></tr>";
            }
        }
    }
}
