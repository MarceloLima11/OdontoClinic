using StackExchange.Redis;
using Application.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService()
        {
            _database = ConnectionMultiplexer.Connect("localhost").GetDatabase();
        }

        public async Task<string> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }
    }
}
