using Newtonsoft.Json;
using System.IO;

namespace ComparisonContextMenu
{
    public static class BaseConfigurationManager
    {
        public static T Get<T>(string storageFilePath)
        {
            Utility.CreateIfNotExist(storageFilePath);

            string text = File.ReadAllText(storageFilePath);

            return JsonConvert.DeserializeObject<T>(text);
        }

        public static void Set<T>(string storageFilePath, T item)
        {
            Utility.CreateIfNotExist(storageFilePath);

            string text = JsonConvert.SerializeObject(item, Formatting.Indented);

            File.WriteAllText(storageFilePath, text);
        }
    }
}