using AuroraLib.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    /// <summary>
    /// Should be created as a singleton and will be used to create an instance of AI model.
    /// </summary>
    public static class AiModelFactory
    {
        public static AiModel Create()
        {
            return new AiModel(DirectoryPaths.GetAiModelPath());
        }
    }
}
