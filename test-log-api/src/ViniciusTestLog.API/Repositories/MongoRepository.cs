using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViniciusTestLog.API.Domain;
using ViniciusTestLog.API.Models;

namespace ViniciusTestLog.API.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<LogData> _logsCollection;

        private readonly IMongoCollection<CategoryData> _categoryCollection;

        public MongoRepository(
            IOptions<LoggingDatabaseSettings> logColletionDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                logColletionDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                logColletionDatabaseSettings.Value.DatabaseName);

            _logsCollection = mongoDatabase.GetCollection<LogData>(
                logColletionDatabaseSettings.Value.LoggingCollectionName);

            _categoryCollection = mongoDatabase.GetCollection<CategoryData>(
                logColletionDatabaseSettings.Value.CategoryCollectionName);
        }

        public async Task CreateBulkLogsAsync(IEnumerable<LogData> logs) =>
            await _logsCollection.InsertManyAsync(logs);

        public async Task CreateLogAsync(LogData log) =>
            await _logsCollection.InsertOneAsync(log);

        public async Task RemoveAllAsync() =>
            await _logsCollection.DeleteManyAsync(Builders<LogData>.Filter.Empty);

        public async Task<IEnumerable<CategoryData>> GetAllCategories() =>
            await _categoryCollection.Find(_ => true).ToListAsync();

        public async Task CreateBulkCategoriesAsync(IEnumerable<CategoryData> categories) =>
            await _categoryCollection.InsertManyAsync(categories);

        public async Task<IEnumerable<LogData>> GetFilteredLogs(LogDataFilter filter)
        {
            var filters = new List<FilterDefinition<LogData>>();

            if (!string.IsNullOrEmpty(filter.Ip))
                filters.Add(Builders<LogData>.Filter.Where(f => f.Ip.ToLower().Contains(filter.Ip.ToLower())));
            if (!string.IsNullOrEmpty(filter.Category))
                filters.Add(Builders<LogData>.Filter.Where(f => f.Category.ToLower().Contains(filter.Category.ToLower())));
            if (!string.IsNullOrEmpty(filter.Message))
                filters.Add(Builders<LogData>.Filter.Where(f => f.Message.ToLower().Contains(filter.Message.ToLower())));
            if (filter.Start != null)
                filters.Add(Builders<LogData>.Filter.Gte(f => f.Date, filter.Start));
            if (filter.Start != null)
                filters.Add(Builders<LogData>.Filter.Lt(f => f.Date, filter.End));

            if (filters.Any())
            {
                var filterMongo = Builders<LogData>.Filter.And(filters);
                return await _logsCollection.Find(filterMongo).Skip((filter.Page - 1) * filter.Quantity).Limit(filter.Quantity).ToListAsync();
            }
            else return await _logsCollection.Find(_ => true).Skip((filter.Page - 1) * filter.Quantity).Limit(filter.Quantity).ToListAsync();
        }
    }
}
