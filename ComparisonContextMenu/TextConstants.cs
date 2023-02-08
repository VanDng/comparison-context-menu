namespace ComparisonContextMenu
{
    public static class TextConstants
    {
        public const string SelectToCompare = "Select to compare";
        public const string Compare = "Compare";
        public const string CompareWindow = "Compare window";
        public static string BadCompareToolConfig(string configPath) => $"Please check the compare tool path at path {configPath}";
        public static string CompareExecutionFail(string configPath) => $"Something went wrong. Check out the log file at path {configPath}";
    }
}