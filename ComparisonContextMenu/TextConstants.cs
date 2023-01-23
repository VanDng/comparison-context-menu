namespace ComparisonContextMenu
{
    public static class TextConstants
    {
        public const string SelectToCompare = "Select to compare";
        public const string Compare = "Compare";
        public static string RiderPathBad(string configPath) => $"It seems Rider's path is not configured. Check out the config file at path {configPath}";
        public static string RiderExecutionFail(string configPath) => $"Something went wrong. Check out the log file at path {configPath}";
    }
}