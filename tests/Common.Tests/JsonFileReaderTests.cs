using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.IO;
using TestsCommon;
using Xunit;

namespace Common.Tests
{
    public class JsonFileReaderTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void GetObject_ShouldThrowException_WhenFileDoesntExit()
        {
            // Arrange
            JsonFileReader uut = new JsonFileReader();

            // Act
            Action act = () => uut.GetObject<object>("testPath");

            // Assert
            act.Should().Throw<FileNotFoundException>();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetObject_ShouldReturnObject()
        {
            // Arrange
            JsonFileReader uut = new JsonFileReader();

            // Act
            JsonReaderTestClass? result = uut.GetObject<JsonReaderTestClass>(TestData.GetFullResourceDirectory("jsonReaderTestData.json"));

            // Assert
            result.Should().NotBeNull();
            using (new AssertionScope())
            {
                result!.UserId.Should().Be("testUserId");
                result!.Password.Should().Be("testPassword");
            }
        }

        private class JsonReaderTestClass
        {
            public string UserId { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}