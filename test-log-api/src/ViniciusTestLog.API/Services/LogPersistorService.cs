using System.Collections.Concurrent;
using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Extensions;
using ViniciusTestLog.API.Repositories;

namespace ViniciusTestLog.API.Services
{
    public class LogPersistorService : ILogPersistorService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMongoRepository _mongoRepository;
        private readonly ILogger<LogPersistorService> _logger;
        public LogPersistorService(IMongoRepository mongoRepository, IWebHostEnvironment webHostEnvironment, ILogger<LogPersistorService> logger)
        {
            _mongoRepository = mongoRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        private async Task<IEnumerable<LogData>> GetLogEntries()
        {
            var logData = new ConcurrentBag<LogData>();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "txt", "auth.log");


            var lines = await File.ReadAllLinesAsync(path);

            Parallel.ForEach(lines, logLine =>
            {
                try
                {
                    var convertedLog = logLine.ToLogData();
                    logData.Add(convertedLog);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{logLine} - {ex.Message}");
                }
            });

            return logData;
        }

        public async Task PersistLogFile()
        {
            await _mongoRepository.RemoveAllAsync();
            var logs = await GetLogEntries();

            var categories = logs.GroupBy(g => g.Category)
                .Select(s =>
                {
                    var last = s.LastOrDefault();
                    return new CategoryData
                    {
                        Name = s.Key,
                        LastIp = last?.Ip ?? "none",
                        LastDate = last?.Date,
                        LastMessage = last?.Message ?? "none",
                        Logs = s
                    };
                }).ToArray();

            await _mongoRepository.CreateBulkLogsAsync(categories);
        }
    }
}
