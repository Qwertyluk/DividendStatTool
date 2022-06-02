using Common.Contracts;
using CommonUI.Contracts;
using CommonUI.ViewModels;
using FluentAssertions;
using FluentAssertions.Events;
using FluentAssertions.Execution;
using Moq;
using System;
using System.ComponentModel;
using Xunit;

namespace CommonUI.Tests.ViewModels
{
    public class BackgroundWorkerViewModelTests
    {
        private readonly Mock<IBackgroundWorkerService> mockWorker = new Mock<IBackgroundWorkerService>();
        private readonly Mock<IMessageHandler> mockMessageHandler = new Mock<IMessageHandler>();

        [Fact]
        public void ViewModel_ShouldReportProgress()
        {
            // Arrange
            int expectedProgress = 2;
            mockWorker.Setup(m => m.Run(
                It.IsAny<DoWorkEventHandler>(),
                It.IsAny<ProgressChangedEventHandler>(),
                It.IsAny<RunWorkerCompletedEventHandler>()))
                .Callback<DoWorkEventHandler, ProgressChangedEventHandler, RunWorkerCompletedEventHandler>(
                (x1, x2, x3) => x2(null, new ProgressChangedEventArgs(expectedProgress, null)));
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);

            // Act & Assert
            using (IMonitor<BackgroundWorkerViewModel> monitor = uut.Monitor())
            {
                uut.StartWork();
                uut.Progress.Should().Be(expectedProgress);
                monitor.Should().RaisePropertyChangeFor(uut => uut.Progress);
            }
        }

        [Fact]
        public void ViewModel_ShouldCancelWorker()
        {
            // Arrange
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);

            // Act
            uut.Cancel();

            // Assert
            mockWorker.Verify(m => m.Cancel(), Times.Once());
        }

        [Fact]
        public void ViewModel_ShouldDisplayMessageAndResetProgress_WhenCompleted()
        {
            // Arrange
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);
            mockWorker.Setup(m => m.Run(
                It.IsAny<DoWorkEventHandler>(),
                It.IsAny<ProgressChangedEventHandler>(),
                It.IsAny<RunWorkerCompletedEventHandler>()))
                .Callback<DoWorkEventHandler, ProgressChangedEventHandler, RunWorkerCompletedEventHandler>(
                (x1, x2, x3) => x3(null, new RunWorkerCompletedEventArgs(null, null, false)));

            // Act
            uut.StartWork();

            // Assert
            using (new AssertionScope())
            {
                mockMessageHandler.Verify(m => m.HandleInfo("Information.", "Process completed successfully."), Times.Once);
                uut.Progress.Should().Be(0);
                uut.DialogResult.Should().BeTrue();
            }
        }

        [Fact]
        public void ViewModel_ShouldDisplayMessageAndResetProgress_WhenCanceled()
        {
            // Arrange
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);
            mockWorker.Setup(m => m.Run(
                It.IsAny<DoWorkEventHandler>(),
                It.IsAny<ProgressChangedEventHandler>(),
                It.IsAny<RunWorkerCompletedEventHandler>()))
                .Callback<DoWorkEventHandler, ProgressChangedEventHandler, RunWorkerCompletedEventHandler>(
                (x1, x2, x3) => x3(null, new RunWorkerCompletedEventArgs(null, null, true)));

            // Act
            uut.StartWork();


            // Assert
            using (new AssertionScope())
            {
                mockMessageHandler.Verify(m => m.HandleInfo("Information.", "Process canceled."), Times.Once);
                uut.Progress.Should().Be(0);
                uut.DialogResult.Should().BeFalse();
            }
        }

        [Fact]
        public void ViewModel_ShouldSetResult_WhenCompletedSuccessfully()
        {
            // Arrange
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);
            int expectedResult = 5;
            mockWorker.Setup(m => m.Run(
                It.IsAny<DoWorkEventHandler>(),
                It.IsAny<ProgressChangedEventHandler>(),
                It.IsAny<RunWorkerCompletedEventHandler>()))
                .Callback<DoWorkEventHandler, ProgressChangedEventHandler, RunWorkerCompletedEventHandler>(
                (x1, x2, x3) => x3(null, new RunWorkerCompletedEventArgs(expectedResult, null, false)));

            // Act
            uut.StartWork();


            // Assert
            uut.BackgroundWorkerResult.Should().Be(expectedResult);
        }

        [Fact]
        public void ViewModel_ShouldNotSetResult_WhenCanceled()
        {
            // Arrange
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);
            mockWorker.Setup(m => m.Run(
                It.IsAny<DoWorkEventHandler>(),
                It.IsAny<ProgressChangedEventHandler>(),
                It.IsAny<RunWorkerCompletedEventHandler>()))
                .Callback<DoWorkEventHandler, ProgressChangedEventHandler, RunWorkerCompletedEventHandler>(
                (x1, x2, x3) => x3(null, new RunWorkerCompletedEventArgs(5, null, true)));

            // Act
            uut.StartWork();


            // Assert
            uut.BackgroundWorkerResult.Should().BeNull();
        }

        [Fact]
        public void ViewModel_ShouldNotSetResult_WhenFailed()
        {
            // Arrange
            BackgroundWorkerViewModel uut = new BackgroundWorkerViewModel(mockWorker.Object, mockMessageHandler.Object);
            mockWorker.Setup(m => m.Run(
                It.IsAny<DoWorkEventHandler>(),
                It.IsAny<ProgressChangedEventHandler>(),
                It.IsAny<RunWorkerCompletedEventHandler>()))
                .Callback<DoWorkEventHandler, ProgressChangedEventHandler, RunWorkerCompletedEventHandler>(
                (x1, x2, x3) => x3(null, new RunWorkerCompletedEventArgs(5, new Exception(), false)));

            // Act
            uut.StartWork();

            // Assert
            uut.BackgroundWorkerResult.Should().BeNull();
        }
    }
}
