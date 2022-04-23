namespace TestsCommon
{
    public static class TestData
    {
        private static readonly string TestResourcesFolderName = "testData";

        public static string GetFullResourceDirectory(string relativePath)
        {
            return Path.Combine(GetResourceDirectory(), relativePath);
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
                throw new InvalidOperationException("Can't find resource directory.");
            }

            return Path.Combine(currentDirectory.FullName, TestResourcesFolderName);
        }
    }
}