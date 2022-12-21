using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Models;
using ViniciusTestLog.API.Repositories;

namespace ViniciusTestLog.API.Services
{
    public class LogQueryService : ILogQueryService
    {
        private readonly IMongoRepository _mongoRepository;
        public LogQueryService(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }
        public async Task<string[]> GetCategories()
        {
            return (await _mongoRepository.GetAllCategories()).Select(s => s.Name ?? string.Empty).ToArray();
        }

        public async Task<IEnumerable<LogData>> GetFiltered(LogDataFilter logDataFilter)
        {
            return await _mongoRepository.GetFilteredLogs(logDataFilter);
        }
    }
}
