using Common;
using CommonUI;
using DividendStatTool.Commands.CommandProviders;
using DividendStatTool.Commands.Factories;
using DividendStatTool.Services;
using DividendStatTool.ViewModels;
using DividendStatTool.Views;
using DividendStatToolLibrary;
using System.Windows;
using xAPIServices;

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
                    new ProviderCommandSymbolsFromFile(
                        new OpenFileDialogService(),
                        new FileReader(),
                        new FilterSymbolsFromFile()),
                    new ProviderCommandSymbolsFromXTB(
                        new UserCredentialsWindowProvider(),
                        new XTBService(),
                        new FilterSymbolsFromXTB(),
                        new MessageBoxWrapper()))
            };
            mainWindow.Show();
        }
    }
}
