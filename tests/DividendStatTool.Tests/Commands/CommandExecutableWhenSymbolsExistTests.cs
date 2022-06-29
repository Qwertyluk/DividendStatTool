using DividendStatTool.Commands;
using DividendStatTool.ViewModels.Contracts;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace DividendStatTool.Tests.Commands
{
    public class CommandExecutableWhenSymbolsExistTests
    {
        [Theory]
        [MemberData(nameof(CanExecute_ShouldReturnTrue_WhenSymbolIsAdded_TestData))]
        public void CanExecute_ShouldReturnTrue_WhenSymbolIsAdded(object commandAsObject, object viewModelAsObject)
        {
            // Arrange
            CommandExecutableWhenSymbolsExist command = (CommandExecutableWhenSymbolsExist)commandAsObject;
            IMainWindowViewModel viewModel = (IMainWindowViewModel)viewModelAsObject;
            viewModel.Symbols.Add("testSymbol");

            // Act
            bool result = command.CanExecute(null);

            // Assert
            result.Should().BeTrue();
        }

        public static IEnumerable<object[]> CanExecute_ShouldReturnTrue_WhenSymbolIsAdded_TestData()
        {
            IMainWindowViewModel viewModel = GetViewModel();

            foreach (var command in GetCommands(viewModel))
            {
                yield return new object[] { command, viewModel };
            }
        }

        [Theory]
        [MemberData(nameof(CanExecute_ShouldBeFalse_WhenSymbolsDontExist_TestData))]
        public void CanExecute_ShouldBeFalse_WhenSymbolsDontExist(object commandAsObject)
        {
            // Arrange
            CommandExecutableWhenSymbolsExist command = (CommandExecutableWhenSymbolsExist)commandAsObject;

            // Act
            bool result = command.CanExecute(null);

            // Assert
            result.Should().BeFalse();
        }

        public static IEnumerable<object[]> CanExecute_ShouldBeFalse_WhenSymbolsDontExist_TestData()
        {
            IMainWindowViewModel viewModel = GetViewModel();

            foreach (var command in GetCommands(viewModel))
            {
                yield return new object[] { command };
            }
        }

        private static IMainWindowViewModel GetViewModel()
        {
            Mock<IMainWindowViewModel> mockViewModel = new Mock<IMainWindowViewModel>();
            mockViewModel.SetupGet(m => m.Symbols).Returns(new BindingList<string>());
            return mockViewModel.Object;
        }

        private static CommandExecutableWhenSymbolsExist[] GetCommands(IMainWindowViewModel viewModel)
        {
            return new CommandExecutableWhenSymbolsExist[]
            {
                new CommandRunCalculations(viewModel, null, null, null, null),
                new CommandSymbolsWriteToFile(viewModel, null, null, null)
            };
        }
    }
}
