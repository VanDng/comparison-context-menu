using System;
using System.IO;

namespace ComparisonContextMenu
{
    public static class AppDataManager
    {
        public static readonly string StorageFilePath =
           Path.Combine
           (
               Utility.AppDataPath(),
               "appData.json"
           );

        public static AppDataItem Get()
            => BaseConfigurationManager.Get<AppDataItem>(StorageFilePath);

        public static void Set(AppDataItem item)
            => BaseConfigurationManager.Set<AppDataItem>(StorageFilePath, item);
    }

    public class AppDataItem
    {
        public string RiderPath { get; set; }
    }
}
