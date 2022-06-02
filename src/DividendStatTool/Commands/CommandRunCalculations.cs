﻿using CommonUI;
using CommonUI.Contracts;
using DividendScrapper;
using DividendScrapper.Data;
using DividendScrapper.Exceptions;
using DividendScrapper.Factories.Contracts;
using DividendStatTool.Data;
using DividendStatTool.ViewModels.Contracts;
using DividendStatToolLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DividendStatTool.Commands
{
    internal class CommandRunCalculations : CommandExecutableWhenSymbolsExist
    {
        private readonly IBackgroundWorkerWindowWrapper bwWindow;
        private readonly IScrapperFactory scrapperFactory;

        public CommandRunCalculations(IMainWindowViewModel viewModel, IBackgroundWorkerWindowWrapper bwWindow, IScrapperFactory scrapperFactory) : base(viewModel)
        {
            this.bwWindow = bwWindow;
            this.scrapperFactory = scrapperFactory;
        }

        #region ICommand

        public override void Execute(object? parameter)
        {
            object? result = bwWindow.GetResult((sender, e) =>
            {
                if (sender is BackgroundWorker worker)
                {
                    SymbolMeasurement[] symbolMeasurements = new SymbolMeasurement[viewModel.Symbols.Count];
                    int i = 0;
                    foreach (var symbol in viewModel.Symbols)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        Scrapper scrapper = scrapperFactory.GetScrapper(symbol);
                        Measurement[] results = Array.Empty<Measurement>();
                        List<string> notFoundSymbols = new List<string>();
                        try
                        {
                            results = scrapper.Scrap();
                        }
                        catch (TextScrapException)
                        {
                            notFoundSymbols.Add(symbol);
                        }
                        symbolMeasurements[i++] = new SymbolMeasurement(symbol, results);
                        int progress = (int)(100.0 * i / viewModel.Symbols.Count);
                        worker.ReportProgress(progress);
                    }
                    e.Result = symbolMeasurements;
                }
            });

            SymbolMeasurement[] measurementResults = result as SymbolMeasurement[] ?? Array.Empty<SymbolMeasurement>();

            // make ranking

            // show results
        }

        #endregion
    }
}
