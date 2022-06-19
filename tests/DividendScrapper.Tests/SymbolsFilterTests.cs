using DividendScrapper.Data;
using DividendScrapper.Enums;
using DividendStatToolLibrary.Data;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace DividendScrapper.Tests
{
    public class SymbolsFilterTests
    {
        [Fact]
        public void Filter_ShouldFilter()
        {
            // Arrange
            SymbolMeasurement validSymbolMeasurement = new SymbolMeasurement(
                "testSymbol1",
                new Measurement[]
                {
                    new Measurement(Factor.DebtPerEquity, 0, (d) => d == 0, null!),
                    new Measurement(Factor.DividendYield, 0, (d) => d == 0, null!)
                });
            SymbolMeasurement invalidSymbolMeasurement = new SymbolMeasurement(
                "testSymbol2",
                new Measurement[]
                {
                    new Measurement(Factor.DebtPerEquity, 0, (d) => d == 0, null!),
                    new Measurement(Factor.DividendYield, 0, (d) => d != 0, null!)
                });
            List<SymbolMeasurement> symbolMeasurements = new List<SymbolMeasurement>()
            {
                validSymbolMeasurement,
                invalidSymbolMeasurement
            };

            SymbolsFilter uut = new SymbolsFilter();


            // Act
            IEnumerable<SymbolMeasurement> output = uut.Filter(symbolMeasurements);

            // Assert
            output.Should().Contain(validSymbolMeasurement);
        }
    }
}
