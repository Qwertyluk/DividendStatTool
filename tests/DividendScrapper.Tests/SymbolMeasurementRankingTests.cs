using DividendScrapper.Data;
using DividendScrapper.Enums;
using DividendScrapper.Factories;
using DividendStatToolLibrary;
using DividendStatToolLibrary.Data;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DividendScrapper.Tests
{
    public class SymbolMeasurementRankingTests
    {
        public static IEnumerable<object[]> AssignRanks_TestData()
        {
            Func<Measurement, Measurement?, int> compGreaterIsBetter = (c, m) => c.Value.CompareTo(m?.Value);
            Func<Measurement, Measurement?, int> compLessIsBetter = (c, m) => c.Value.CompareTo(m?.Value) * -1;

            return new List<object[]>
            {
                new object[]
                {
                    new Measurement[]
                    {
                        new MeasurementFactory(null!, compGreaterIsBetter).CreateMeasurement(Factor.DividendPayoutRatio, 1),
                        new MeasurementFactory(null!, compLessIsBetter).CreateMeasurement(Factor.DebtPerEquity, 0)
                    },
                    new Measurement[]
                    {
                        new MeasurementFactory(null!, compGreaterIsBetter).CreateMeasurement(Factor.DividendPayoutRatio, 0),
                        new MeasurementFactory(null!, compLessIsBetter).CreateMeasurement(Factor.DebtPerEquity, 1),
                    },
                    0
                },
                new object[]
                {
                    new Measurement[]
                    {
                        new MeasurementFactory(null!, compGreaterIsBetter).CreateMeasurement(Factor.DividendPayoutRatio, 0),
                        new MeasurementFactory(null!, compLessIsBetter).CreateMeasurement(Factor.DebtPerEquity, 1),
                    },
                    new Measurement[]
                    {
                        new MeasurementFactory(null!, compGreaterIsBetter).CreateMeasurement(Factor.DividendPayoutRatio, 1),
                        new MeasurementFactory(null!, compLessIsBetter).CreateMeasurement(Factor.DebtPerEquity, 0)
                    },
                    1
                }
            };
        }

        [Theory]
        [MemberData(nameof(AssignRanks_TestData))]
        public void AssignRanks_ShouldCorrectlyAssignRanks(
            Measurement[] symbolMeasurements1,
            Measurement[] symbolMeasurements2,
            int betterSymbolIndex)
        {
            // Arrange
            SymbolMeasurement[] input = new SymbolMeasurement[]
            {
                new SymbolMeasurement("symbol1", symbolMeasurements1),
                new SymbolMeasurement("symbol2", symbolMeasurements2)
            };
            SymbolsRanking uut = new SymbolsRanking();

            // Act
            var assignedRanks = uut.AssignRanks(input);
            assignedRanks.TryGetValue(0, out SymbolMeasurement? outputBetterSymbol);

            // Assert
            assignedRanks.Should().ContainValues(input);
            outputBetterSymbol.Should().BeSameAs(input[betterSymbolIndex]);
        }
    }
}
