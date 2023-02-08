using System.IO;

namespace ComparisonContextMenu
{
    public static class AppConfigManager
    {
        public static readonly string StorageFilePath =
           Path.Combine
           (
               Utility.AppDataPath(),
               "appConfig.json"
           );

        public static AppDataItem Get()
            => BaseConfigurationManager.Get<AppDataItem>(StorageFilePath);

        public static void Set(AppDataItem item)
            => BaseConfigurationManager.Set<AppDataItem>(StorageFilePath, item);

        public static void SetDefaultIfEmpty()
        {
            AppDataItem item = Get();

            if (item is null)
            {
                item = new AppDataItem()
                {
                    CompareToolArg = Rider.RiderArg(),
                    CompareToolPath = Rider.RiderPath()
                };
            }

            Set(item);
        }
    }

    public class AppDataItem
    {
        public string CompareToolPath { get; set; }
        public string CompareToolArg { get; set; }
    }
}
