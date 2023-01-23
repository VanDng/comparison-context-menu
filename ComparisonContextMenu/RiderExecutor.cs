using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ComparisonContextMenu
{
    public static class RiderExecutor
    {
        public static void Execute(string itemPathOne, string itemPathTwo)
        {
            try
            {
                string riderPath = GetRiderPath();

                if (string.IsNullOrEmpty(riderPath))
                {
                    MessageBox.Show(TextConstants.RiderPathBad(AppDataManager.StorageFilePath));
                    return;
                }

                string arg = $"diff {itemPathOne} {itemPathTwo}";
                Process.Start(riderPath, arg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(TextConstants.RiderExecutionFail(LogManager.LogPath));
                LogManager.WriteLine(ex);
            }
        }

        private static string GetRiderPath()
        {
            AppDataItem item = AppDataManager.Get();

            if (item is null)
            {
                item = new AppDataItem()
                {
                    RiderPath = TryFindRiderLocation()
                };

                AppDataManager.Set(item);

            }

            return File.Exists(item.RiderPath)
                ? item.RiderPath
                : null;
        }

        public static string TryFindRiderLocation()
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
    }
}