using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Models;

namespace ViniciusTestLog.API.Services
{
    public interface ILogQueryService
    {
        Task<string[]> GetCategories();
        Task<IEnumerable<CategoryData>> GetFiltered(LogDataFilter logDataFilter);

    }
}
