namespace ViniciusTestLog.API.Models
{
    public class LoggingDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string LoggingCollectionName { get; set; } = null!;
    }
}
