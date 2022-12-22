namespace ViniciusTestLog.API.Domain
{
    public class LogData
    {
        public DateTime Date { get; set; }
        public string? Ip { get; set; }
        public string? Category { get; set; }
        public string? Message { get; set; }
    }
}
