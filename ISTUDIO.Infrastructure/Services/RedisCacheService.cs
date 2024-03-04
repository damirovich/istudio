using StackExchange.Redis;
using System.Text.Json;

namespace ISTUDIO.Infrastructure.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly ConnectionMultiplexer _redisConnection;

    public RedisCacheService(string redisConnectionString)
    {
        _redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
    }
    public async Task<T> GetAsync<T>(string key)
    {
        var database = _redisConnection.GetDatabase();
        var value = await database.StringGetAsync(key);

        return value.IsNull ? default : JsonSerializer.Deserialize<T>(value);
    }
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var database = _redisConnection.GetDatabase();
        var serializedValue = JsonSerializer.Serialize(value);

        await database.StringSetAsync(key, serializedValue, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        var database = _redisConnection.GetDatabase();
        await database.KeyDeleteAsync(key);
    }
}
