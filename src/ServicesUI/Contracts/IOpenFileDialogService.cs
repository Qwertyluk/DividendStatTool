namespace DividendStatTool.Services.Contracts
{
    public interface IOpenFileDialogService
    {
        string FileName { get; set; }
        bool? ShowDialog();
    }
}
