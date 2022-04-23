using Newtonsoft.Json;

namespace Common
{
    public class JsonFileReader
    {
        public T? GetObject<T>(string path)
        {
            string fileContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}
