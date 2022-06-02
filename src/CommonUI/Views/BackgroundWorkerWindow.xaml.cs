using Common;
using CommonUI.ViewModels;
using CommonUI.ViewModels.Contracts;
using System.Windows;

namespace CommonUI.Views
{
    /// <summary>
    /// Interaction logic for BackgroundWorkerWindow.xaml
    /// </summary>
    public partial class BackgroundWorkerWindow : Window, IBackgroundWorkerWindow
    {
        public IBackgroundWorkerViewModel ViewModel { get; }

        public BackgroundWorkerWindow()
        {
            InitializeComponent();
            ViewModel = new BackgroundWorkerViewModel(new BackgroundWorkerService(), new MessageBoxWrapper());
            DataContext = ViewModel;
            Loaded += BackgroundWorkerWindow_Loaded;
        }

        private void BackgroundWorkerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.StartWork();
        }
    }
}
