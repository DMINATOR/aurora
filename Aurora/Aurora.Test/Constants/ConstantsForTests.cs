using AuroraLib.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Test.Constants
{
    public static class ConstantsForTests
    {
        public const string OLLAMA_URL = "http://localhost:11434";

        public static AiModelBase OLLAMA_MODEL = new Phi4AiModel();
    }
}
