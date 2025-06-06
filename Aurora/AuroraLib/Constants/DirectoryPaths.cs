﻿namespace AuroraLib.Constants
{
    /// <summary>
    /// Defines paths for all potential locations that are used by the lib
    /// </summary>
    public static class DirectoryPaths
    {
        public static string GetCurrentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetOllamaPath()
        {
            return System.IO.Path.Combine(GetCurrentPath(), "Ollama", "ollama.exe");
        }

        public static string GetAiModelPath()
        {
            return System.IO.Path.Combine(GetCurrentPath(), "Models", "AiModel.onnx");
        }
    }
}
