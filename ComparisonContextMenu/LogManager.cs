using System;
using System.IO;
namespace ComparisonContextMenu
{
    public static class LogManager
    {
        public static readonly string LogPath = Path.Combine(Utility.AppDataPath(), "log.txt");

        public static void WriteLine(string message)
        {
            Utility.CreateIfNotExist(LogPath);

            string formatMessage = $"{DateTime.Now.ToString("dd:mm:yyyy HH:mm:ss")} {message}{Environment.NewLine}";

            File.AppendAllText(LogPath, formatMessage);
        }

        public static void WriteLine(Exception exception)
        {
            string message = $"{exception.Message} {exception.StackTrace} {exception.InnerException?.Message ?? string.Empty}";
            WriteLine(message);
        }
    }
}
