using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Models;

namespace ViniciusTestLog.API.Repositories
{
    public interface IMongoRepository
    {
        Task CreateBulkLogsAsync(IEnumerable<LogData> logs);
        Task CreateBulkCategoriesAsync(IEnumerable<CategoryData> categories);
        Task CreateLogAsync(LogData log);
        Task RemoveAllAsync();
        Task<IEnumerable<CategoryData>> GetAllCategories();
        Task<IEnumerable<LogData>> GetFilteredLogs(LogDataFilter filter);
    }
}
