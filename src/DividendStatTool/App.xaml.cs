using DividendCalculation.Commands.Factories;
using DividendCalculation.Services;
using DividendCalculation.ViewModels;
using DividendCalculation.Views;
using DividendStatToolLibrary;
using System.Windows;

namespace DividendStatTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(
                    new FetchSymbolsFromFileCommandFactory(
                        new SymbolsProviderFromFile(
                            new OpenFileDialogService())))
            };
            mainWindow.Show();
        }
    }
}
