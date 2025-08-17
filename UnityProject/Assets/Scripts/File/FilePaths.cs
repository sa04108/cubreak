using System.IO;
using UnityEngine;

namespace Cubreak
{
    public class FilePaths
    {
        public const string StageJsonPath = "Resources/stage_data.json";
        public const string StageExJsonPath = "Resources/stage_data_ex.json";

        public static string GetResourcesRelativePath(string path)
        {
            int startIdx = path.LastIndexOf("Resources/") + 1;
            return Path.GetFileNameWithoutExtension(path.Substring(startIdx));
        }
    }

}