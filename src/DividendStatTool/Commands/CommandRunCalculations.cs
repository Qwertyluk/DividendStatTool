using CommonUI.Contracts;
using DividendScrapper.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace DividendStatTool.Commands
{
    internal class CommandRunCalculations : CommandExecutableWhenSymbolsExist
    {
        private readonly IBackgroundWorkerWindowWrapper bwWindow;
        private readonly ISymbolsScrapper symbolsScrapper;
        private readonly ISymbolMeasurementsFilter symbolsFilter;
        private readonly ISymbolsRanking symbolsRanking;

        public CommandRunCalculations(
            IMainWindowViewModel viewModel,
            IBackgroundWorkerWindowWrapper bwWindow,
            ISymbolsScrapper symbolsScrapper,
            ISymbolMeasurementsFilter symbolsFilter,
            ISymbolsRanking symbolsRanking) : base(viewModel)
        {
            this.bwWindow = bwWindow;
            this.symbolsScrapper = symbolsScrapper;
            this.symbolsFilter = symbolsFilter;
            this.symbolsRanking = symbolsRanking;
        }

        #region ICommand

        public override void Execute(object? parameter)
        {
            object? result = bwWindow.GetResult((sender, e) =>
            {
                if (sender is BackgroundWorker worker)
                {
                    symbolsScrapper.CallBackProgress = progress => worker.ReportProgress(progress);
                    symbolsScrapper.CallBackCancel = () =>
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return true;
                        }

                        return false;
                    };
                    e.Result = symbolsScrapper.GetSymbolsMeasurements(viewModel.Symbols);
                }
            });

            if (result is IEnumerable<SymbolMeasurement> measurements)
            {
                IEnumerable<SymbolMeasurement> filteredMeasurements = symbolsFilter.Filter(measurements);
                SortedList<int, SymbolMeasurement> rankedMeasuremenets = symbolsRanking.AssignRanks(filteredMeasurements);
            }

            // TODO: show results

        }

        #endregion
    }
}
