using Microsoft.VisualBasic;

namespace ViniciusTestLog.API.Models
{
    public class LogDataFilter
    {
        public string? Category { get; set; }
        public string? Message { get; set; }
        public string? Ip { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int Quantity { get; set; } = 50;
        public int Page { get; set; } = 1;
    }
}
