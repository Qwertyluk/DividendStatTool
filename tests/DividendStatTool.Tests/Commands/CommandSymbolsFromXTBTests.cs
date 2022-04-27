using CommonUI.Contracts;
using CommonUI.Models;
using DividendStatTool.Commands;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Contracts;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System.ComponentModel;
using xAPIServices.Contracts;
using xAPIServices.Enums;
using xAPIServices.Exceptions;
using Xunit;

namespace DividendStatTool.Tests.Commands
{
    public class CommandSymbolsFromXTBTests
    {
        private readonly Mock<IMainWindowViewModel> mockViewModel = new Mock<IMainWindowViewModel>();
        private readonly Mock<IUserCredentialsProvider> mockUserCredentials = new Mock<IUserCredentialsProvider>();
        private readonly Mock<IXTBService> mockXTBService = new Mock<IXTBService>();
        private readonly Mock<IFilterStringCollection> mockFilter = new Mock<IFilterStringCollection>();
        private readonly Mock<IMessageHandler> mockMessageHandler = new Mock<IMessageHandler>();

        private CommandSymbolsFromXTB GetUUT()
        {
            return new CommandSymbolsFromXTB(
                mockViewModel.Object,
                mockUserCredentials.Object,
                mockXTBService.Object,
                mockFilter.Object,
                mockMessageHandler.Object);
        }

        [Fact]
        public void GetSymbols_ShouldChangeViewModel()
        {
            // Arrange
            mockViewModel.Setup(m => m.Symbols).Returns(new BindingList<string>());
            UserCredentials uc = new UserCredentials()
            {
                UserName = "testUserName",
                Password = "testPassword"
            };
            mockUserCredentials.Setup(m => m.GetUserCredentials()).Returns(uc);
            BindingList<string> receivedSymbols = new BindingList<string>() { "testReceived1", "testReceived2" };
            mockXTBService.Setup(m => m.GetSymbols(SymbolsGroupName.US)).Returns(receivedSymbols);
            BindingList<string> filteredSymbols = new BindingList<string>() { "testFiltered1", "testFiltered2" };
            mockFilter.Setup(m => m.Filter(receivedSymbols)).Returns(filteredSymbols);
            CommandSymbolsFromXTB uut = GetUUT();

            // Act
            uut.Execute(null);

            // Assert
            mockViewModel.Object.Symbols.Should().Equal(filteredSymbols);
        }

        [Fact]
        public void GetSymbols_ShouldHandleMessage_WhenLoginFailed()
        {
            // Arrange
            BindingList<string> initialSymbols = new BindingList<string>() { "test1", "test2" };
            mockViewModel.SetupGet(m => m.Symbols).Returns(initialSymbols);
            string userName = "testUserName";
            string password = "testPassword";
            UserCredentials uc = new UserCredentials()
            {
                UserName = userName,
                Password = password
            };
            mockUserCredentials.Setup(m => m.GetUserCredentials()).Returns(uc);
            mockXTBService.Setup(m => m.Login(userName, password)).Throws<XTBLoginException>();
            CommandSymbolsFromXTB uut = GetUUT();

            // Act
            uut.Execute(null);

            // Assert
            using (new AssertionScope())
            {
                mockMessageHandler.Verify(m => m.HandleError("Login Failed", "Could not connect to XTB server."), Times.Once);
                mockViewModel.Object.Symbols.Should().Equal(initialSymbols);
            }
        }
    }
}
