using CommonUI.Contracts;
using CommonUI.Factories.Contracts;
using CommonUI.Views;
using DividendScrapper.Contracts;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace DividendStatTool.Commands
{
    internal class CommandRunCalculations : CommandExecutableWhenSymbolsExist
    {
        private readonly IBackgroundWorkerWindowWrapperFactory bwWindowFactory;
        private readonly ISymbolsScrapper symbolsScrapper;
        private readonly ISymbolMeasurementsFilter symbolsFilter;
        private readonly ISymbolsRanking symbolsRanking;

        public CommandRunCalculations(
            IMainWindowViewModel viewModel,
            IBackgroundWorkerWindowWrapperFactory bwWindowFactory,
            ISymbolsScrapper symbolsScrapper,
            ISymbolMeasurementsFilter symbolsFilter,
            ISymbolsRanking symbolsRanking) : base(viewModel)
        {
            this.bwWindowFactory = bwWindowFactory;
            this.symbolsScrapper = symbolsScrapper;
            this.symbolsFilter = symbolsFilter;
            this.symbolsRanking = symbolsRanking;
        }

        #region ICommand

        public override void Execute(object? parameter)
        {
            object? result = bwWindowFactory.GetWindow().GetResult((sender, e) =>
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

                DataGridWindow dataGridWindow = new DataGridWindow();
                dataGridWindow.ViewModel.Data = CreateDataGridModel(rankedMeasuremenets);
                dataGridWindow.Show();
            }
        }

        #endregion

        private static DataTable CreateDataGridModel(SortedList<int, SymbolMeasurement> symbolMeasures)
        {
            DataTable dt = new DataTable();
            string rankingColumnName = "Ranking";
            string symbolColumnName = "Symbol";
            dt.Columns.Add(rankingColumnName);
            dt.Columns.Add(symbolColumnName);

            foreach (var measure in symbolMeasures.Values.First().Measurements)
            {
                dt.Columns.Add(measure.Factor.ToString());
            }

            foreach (var symbolMeasure in symbolMeasures)
            {
                DataRow row = dt.NewRow();
                row[rankingColumnName] = symbolMeasure.Key;
                row[symbolColumnName] = symbolMeasure.Value.SymbolName;

                foreach (var measure in symbolMeasure.Value.Measurements)
                {
                    row[measure.Factor.ToString()] = measure.Value;
                }

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
