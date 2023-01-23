using System;
using System.IO;

namespace ComparisonContextMenu
{
    public static class ComparisonDataManager
    {
        private static readonly string StorageFilePath =
           Path.Combine
           (
               Utility.AppDataPath(),
               "comparisonData.json"
           );

        public static ComparisonItem Get()
            => BaseConfigurationManager.Get<ComparisonItem>(StorageFilePath);

        public static void Set(ComparisonItem item)
            => BaseConfigurationManager.Set<ComparisonItem>(StorageFilePath, item);
    }

    public class ComparisonItem
    {
        public string Path { get; set; }
        public ComparisonItemType Type { get; set; }
    }

    public enum ComparisonItemType
    {
        File,
        Folder
    }
}
