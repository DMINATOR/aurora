namespace Aurora.Test.Constants
{
    /// <summary>
    /// These are paths that will be used for running tests
    /// </summary>
    public static class DirectoryPathsForTests
    {
        public static string GetCurrentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// The root path of the project
        /// </summary>
        /// <returns></returns>
        public static string GetProjectRootPath()
        {
            var root = Directory.GetParent(GetCurrentPath())!.Parent!.Parent!.Parent!.Parent!.Parent;
            return root!.FullName;
        }

        public static string GetOllamaPath()
        {
            return System.IO.Path.Combine(GetProjectRootPath(), "Ollama", "ollama.exe");
        }
    }
}
