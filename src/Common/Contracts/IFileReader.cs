namespace Common.Contracts
{
    public interface IFileReader
    {
        string[] ReadAllLines(string path);
    }
}
