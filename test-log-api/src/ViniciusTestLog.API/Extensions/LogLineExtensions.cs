using System.Globalization;
using ViniciusTestLog.API.Domain;

namespace ViniciusTestLog.API.Extensions
{
    public static class LogLineExtensions
    {
        private const string DATE_FORMAT = "MMM dd HH:mm:ss/yyyy";
        //Assuming current year since log don't have year
        public static LogData ToLogData(this string logData, int year = 2022)
        {
            var log = new LogData();
            DateTime dateValue;

            var dateString = $"{logData.Substring(0, 15)}/{year}";

            if (DateTime.TryParseExact(dateString, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                log.Date = dateValue;
            else
            {
                //Adding 0 to date log
                var formatedDate = dateString.Split(new[] { ' ' }, 2);
                formatedDate[1] = $" 0{formatedDate[1].Trim()}";
                dateString = string.Concat(formatedDate);

                if (DateTime.TryParseExact(dateString, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    log.Date = dateValue;
                else throw new Exception("Invalid Date");
            }

            log.Ip = logData.Substring(19, 13);
            var categorySplit = logData.Substring(33).Split(new[] { ':' }, 2);
            log.Category = categorySplit[0].Trim();
            log.Message = categorySplit[1].Trim();

            return log;
        }
    }
}
