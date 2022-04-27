using FluentAssertions;
using System;
using System.Collections.Generic;
using xAPIServices.Enums;
using xAPIServices.Exceptions;
using xAPIServices.Tests.TestDataProviders;
using Xunit;

namespace xAPIServices.Tests
{
    public class XTBServiceTests
    {
        private XTBService GetUTT()
        {
            return new XTBService(true);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Login_ShouldThrowException_WhenInvalidUserCredentials()
        {
            // Assert
            XTBService uut = GetUTT();

            // Act
            Action act = () => uut.Login("testId", "testPassword");

            // Assert
            act.Should().Throw<XTBLoginException>().WithMessage($"Login failed. Error code: EX000");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetSymbols_ShouldReturnSymbols()
        {
            // Arrange
            UserCredentials uc = UserCredentialsProvider.GetUserCredentials("XTBCredentials.json");
            XTBService uut = GetUTT();
            uut.Login(uc.UserName, uc.Password);

            // Act
            IEnumerable<string> symbols = uut.GetSymbols(SymbolsGroupName.US);

            // Assert
            symbols.Should().NotBeEmpty();
        }
    }
}
