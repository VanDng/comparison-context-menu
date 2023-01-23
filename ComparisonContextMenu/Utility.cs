using System;
using System.IO;

namespace ComparisonContextMenu
{
    public static class Utility
    {
        public static bool IsFolder(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory);
        }

        public static string AppDataPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        public static void CreateIfNotExist(string path)
        {
            string dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);

            if (!File.Exists(path))
                File.Create(path)?.Dispose();
        }
    }
}
