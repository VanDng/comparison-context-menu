using System.IO;
using System.Linq;

namespace ComparisonContextMenu
{
    public static class Rider
    {
        public static string RiderPath()
        {
            string root = @"C:\Program Files\JetBrains\";

            string[] candidates = Directory.GetDirectories(root, "JetBrains Rider*", SearchOption.TopDirectoryOnly);

            string riderFolder =
                candidates
                    .OrderByDescending(candidate => candidate)
                    .FirstOrDefault();

            if (string.IsNullOrEmpty(riderFolder))
                return null;

            string riderPath =
                Path.Combine
                (
                    riderFolder,
                    "bin",
                    "rider64.exe"
                );

            return File.Exists(riderPath)
                ? riderPath
                : null;
        }

        public static string RiderArg()
        {
            return $"diff \"{ArgConstants.FileOne}\" \"{ArgConstants.FileTwo}\"";
        }
    }
}