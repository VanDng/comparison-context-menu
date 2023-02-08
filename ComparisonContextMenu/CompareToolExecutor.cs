using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ComparisonContextMenu
{
    public static class CompareToolExecutor
    {
        public static void Execute(string pathOne, string pathTwo)
        {
            try
            {
                string compareToolPath = GetCompareToolPath();
                string arg = GetCompareArg(pathOne, pathTwo);

                if (string.IsNullOrEmpty(compareToolPath))
                {
                    MessageBox.Show(TextConstants.BadCompareToolConfig(AppConfigManager.StorageFilePath));
                    return;
                }

                Process.Start(compareToolPath, arg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(TextConstants.CompareExecutionFail(LogManager.LogPath));
                LogManager.WriteLine(ex);
            }
        }

        public static void Execute()
        {
            Execute(string.Empty, string.Empty);
        }

        private static string GetCompareArg(string pathOne, string pathTwo)
        {
            AppDataItem item = AppConfigManager.Get();

            return
                item is null ||
                (string.IsNullOrEmpty(pathOne) && string.IsNullOrEmpty(pathTwo))
                    ? string.Empty
                    : item.CompareToolArg
                            .Replace(ArgConstants.FileOne, pathOne)
                            .Replace(ArgConstants.FileTwo, pathTwo);
        }

        private static string GetCompareToolPath()
        {
            AppDataItem item = AppConfigManager.Get();

            if (item is null)
                return null;

            return File.Exists(item.CompareToolPath)
                ? item.CompareToolPath
                : null;
        }
    }
}