
namespace ImdbScraper.Configuration
{
    public static class AppConfiguration
    {
        public const string ListUrl = "https://www.imdb.com/list/ls052283250/";
        public const string DataDir = "Data";
        public const string DataFile = "celebs.json";
        public const string OriginalFile = "celebs.original.json";
        public const int MaxParallelism = 15;
        public const int MinDelayMs = 30;
        public const int MaxDelayMs = 50;
    }

}
