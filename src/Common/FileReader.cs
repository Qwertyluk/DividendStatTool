using Common.Contracts;

namespace Common
{
    public class FileReader : IFileReader
    {
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
