using Common.Extensions;
using FluentAssertions;
using System;
using Xunit;

namespace Common.Tests.Extensions
{
    public class UriExtensionsTests
    {
        [Theory]
        [InlineData("http://www.domain.com/test", "http://www.domain.com/test?param=value", "value")]
        [InlineData("http://www.domain.com/test?param1=value1", "http://www.domain.com/test?param1=value1&param=value", "value")]
        [InlineData("http://www.domain.com/test", "http://www.domain.com/test?param=", "")]
        public void AddParameter_ShouldAddQueryString(string baseUri, string expectedUri, string paramValue)
        {
            // Arrange
            Uri uri = new Uri(baseUri);

            // Act
            Uri result = uri.AddParameter("param", paramValue);

            // Assert
            result.Should().BeEquivalentTo(new Uri(expectedUri));
        }

        [Theory]
        [InlineData(null, "value")]
        [InlineData("name", null)]
        public void AddParameter_WhenArgumentIsNull_ShouldThrowException(string paramName, string paramValue)
        {
            // Arrange
            Uri uri = new Uri("http://www.domain.com/test");

            // Act
            Action act = () => uri.AddParameter(paramName, paramValue);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddParameter_WhenParamNameIsEmpty_ShouldThrowException()
        {
            // Arrange
            Uri uri = new Uri("http://www.domain.com/test");

            // Act
            Action act = () => uri.AddParameter("", "value");

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
