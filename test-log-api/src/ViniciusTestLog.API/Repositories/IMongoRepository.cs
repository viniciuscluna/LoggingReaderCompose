using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Models;

namespace ViniciusTestLog.API.Repositories
{
    public interface IMongoRepository
    {
        Task CreateBulkLogsAsync(IEnumerable<CategoryData> logs);
        Task CreateLogAsync(CategoryData log);
        Task RemoveAllAsync();
        Task<IEnumerable<CategoryData>> GetAllCategories();
        Task<IEnumerable<CategoryData>> GetFilteredLogs(LogDataFilter filter);
    }
}
