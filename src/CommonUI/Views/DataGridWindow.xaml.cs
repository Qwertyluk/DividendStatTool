using CommonUI.ViewModels;
using CommonUI.ViewModels.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CommonUI.Views
{
    /// <summary>
    /// Interaction logic for DataGridWindow.xaml
    /// </summary>
    public partial class DataGridWindow : Window
    {
        public IDataGridViewModel ViewModel { get; }

        public DataGridWindow()
        {
            InitializeComponent();
            ViewModel = new DataGridViewModel();
            DataContext = ViewModel;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            headerStyle.Setters.Add(new Setter()
            {
                Property = HorizontalContentAlignmentProperty,
                Value = HorizontalAlignment.Center
            });

            Style cellStyle = new Style(typeof(DataGridCell));
            cellStyle.Setters.Add(new Setter()
            {
                Property = TextBlock.TextAlignmentProperty,
                Value = TextAlignment.Center
            });

            e.Column.CellStyle = cellStyle;
            e.Column.HeaderStyle = headerStyle;
        }
    }
}
