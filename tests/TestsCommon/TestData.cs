namespace TestsCommon
{
    public static class TestData
    {
        private static readonly string TestResourcesFolderName = "testData";

        public static string CombineWithResourceDirectory(string path)
        {
            return Path.Combine(GetResourceDirectory(), path);
        }
        private static string GetResourceDirectory()
        {
            DirectoryInfo? currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (currentDirectory != null && !Directory.Exists(Path.Combine(currentDirectory.FullName, TestResourcesFolderName)))
            {
                currentDirectory = currentDirectory.Parent;
            }

            if (currentDirectory == null)
            {
                throw new InvalidOperationException("Can't find resource directory");
            }

            return Path.Combine(currentDirectory.FullName, TestResourcesFolderName);
        }
    }
}