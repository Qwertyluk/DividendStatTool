using Common.Extensions;
using FluentAssertions;
using System.ComponentModel;
using Xunit;

namespace Common.Tests.Extensions
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(EnumTest.TestValue1, "TestDescription1")]
        [InlineData(EnumTest.TestValue2, "")]
        [InlineData((EnumTest)99, "")]
        public void GetDescription_ShouldReturnCorrectString(EnumTest e, string expectedResult)
        {
            // Arrange & Act
            string result = e.GetDescription();

            // Assert
            result.Should().Be(expectedResult);
        }

        public enum EnumTest
        {
            [Description("TestDescription1")]
            TestValue1,
            TestValue2
        }
    }
}
