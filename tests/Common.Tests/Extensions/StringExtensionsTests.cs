using Common.Extensions;
using FluentAssertions;
using Xunit;

namespace Common.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("10%", 0.1)]
        [InlineData("10.10%", 0.101)]
        public void PercentageToDouble_ShouldConvert(string percentageAsString, double expectedResult)
        {
            // Arrange & Act
            double result = percentageAsString.PercentageToDouble();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("0.1B", 100_000_000)]
        [InlineData("2B", 2_000_000_000)]
        [InlineData("2.2B", 2_200_000_000)]
        public void BillionAnnotationToDouble_ShouldConvert(string input, double expectedResult)
        {
            // Arrange & Act
            double result = input.BillionAnnotationToDouble();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
