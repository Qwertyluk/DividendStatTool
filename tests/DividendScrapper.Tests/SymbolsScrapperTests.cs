using DividendScrapper.Contracts;
using DividendScrapper.Data;
using DividendScrapper.Enums;
using DividendScrapper.Exceptions;
using DividendScrapper.Factories.Contracts;
using DividendStatToolLibrary.Data;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DividendScrapper.Tests
{
    public class SymbolsScrapperTests
    {
        private readonly SymbolsScrapper uut = new SymbolsScrapper();
        private readonly Mock<IScrapperFactory> mockScrapperFactory = new Mock<IScrapperFactory>();
        private readonly Mock<IScrapper> mockScrapper = new Mock<IScrapper>();
        private readonly string[] symbolNames = new string[] { "testSymbol" };

        private void MockScrapper()
        {
            mockScrapperFactory.Setup(m => m.GetScrapper(symbolNames[0])).Returns(mockScrapper.Object);
            uut.ScrapperFactory = mockScrapperFactory.Object;
        }

        [Fact]
        public void GetSymbolsMeasurements_ShouldCallCallBackCancel()
        {
            // Arrange
            bool isCalled = false;
            uut.CallBackCancel = () =>
            {
                isCalled = true;
                return true;
            };

            // Act
            IEnumerable<SymbolMeasurement> output = uut.GetSymbolsMeasurements(symbolNames);

            // Assert
            isCalled.Should().BeTrue();
            output.Should().BeEmpty();
        }

        [Fact]
        public void GetSymbolsMeasurements_ShouldScrapSymbol()
        {
            // Arrange
            MockScrapper();
            Measurement[] scrappedMeasurement = new Measurement[] { new Measurement(Factor.DebtPerEquity, 0, null!, null!) };
            mockScrapper.Setup(m => m.Scrap()).Returns(scrappedMeasurement);

            // Act
            IEnumerable<SymbolMeasurement> output = uut.GetSymbolsMeasurements(symbolNames);

            // Assert
            output.Should().Contain(s => s.Measurements == scrappedMeasurement);
        }

        [Fact]
        public void GetSymbolsMeasurements_ShouldAddFailedSymbol()
        {
            // Arrange
            MockScrapper();
            mockScrapper.Setup(m => m.Scrap()).Throws<TextScrapException>();

            // Act
            uut.GetSymbolsMeasurements(symbolNames);

            // Assert
            uut.FailedSymbols.Should().Contain(symbolNames);
        }

        [Fact]
        public void GetSymbolsMeasurements_ShouldReportProgress()
        {
            // Arrange
            int finalProgress = 0;
            int count = 0;
            uut.CallBackProgress = i =>
            {
                count++;
                finalProgress = i;
            };

            // Act
            uut.GetSymbolsMeasurements(symbolNames);

            // Assert
            using (new AssertionScope())
            {
                finalProgress.Should().Be(100);
                count.Should().Be(symbolNames.Length);
            }
        }
    }
}
