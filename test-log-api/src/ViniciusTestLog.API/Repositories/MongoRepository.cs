using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Models;

namespace ViniciusTestLog.API.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<CategoryData> _logsCollection;

        public MongoRepository(
            IOptions<LoggingDatabaseSettings> logColletionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                logColletionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                logColletionDatabaseSettings.Value.DatabaseName);

            _logsCollection = mongoDatabase.GetCollection<CategoryData>(
                logColletionDatabaseSettings.Value.LoggingCollectionName);
        }

        public async Task CreateBulkLogsAsync(IEnumerable<CategoryData> logs) =>
            await _logsCollection.InsertManyAsync(logs);

        public async Task CreateLogAsync(CategoryData log) =>
            await _logsCollection.InsertOneAsync(log);

        public async Task RemoveAllAsync() => await _logsCollection.DeleteManyAsync(Builders<CategoryData>.Filter.Empty);
        

        public async Task<IEnumerable<CategoryData>> GetAllCategories() =>
            await _logsCollection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<CategoryData>> GetFilteredLogs(LogDataFilter filter)
        {
            var filters = new List<FilterDefinition<CategoryData>>();

            if (!string.IsNullOrEmpty(filter.Ip))
                filters.Add(Builders<CategoryData>.Filter.Where(f => f.Logs.Any(f2=> f2.Ip.ToLower().Contains(filter.Ip.ToLower()))));
            if (!string.IsNullOrEmpty(filter.Category))
                filters.Add(Builders<CategoryData>.Filter.Where(f => f.Logs.Any(f2 => f2.Category.ToLower().Contains(filter.Category.ToLower()))));
            if (!string.IsNullOrEmpty(filter.Message))
                filters.Add(Builders<CategoryData>.Filter.Where(f => f.Logs.Any(f2 => f2.Message.ToLower().Contains(filter.Message.ToLower()))));
            if (filter.Start != null)
                filters.Add(Builders<CategoryData>.Filter.Gte(f => f.LastDate, filter.Start));
            if (filter.Start != null)
                filters.Add(Builders<CategoryData>.Filter.Lt(f => f.LastDate, filter.End));

            if (filters.Any())
            {
                var filterMongo = Builders<CategoryData>.Filter.And(filters);
                return await _logsCollection.Find(filterMongo).Skip((filter.Page - 1) * filter.Quantity).Limit(filter.Quantity).ToListAsync();
            }
            else return await _logsCollection.Find(_ => true).Skip((filter.Page - 1) * filter.Quantity).Limit(filter.Quantity).ToListAsync();
        }
    }
}
