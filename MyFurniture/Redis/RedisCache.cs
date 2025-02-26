using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MyFurniture.Redis;

public class RedisCache : IRedisCache
{
    private readonly IOptions<RedisCacheSettings> _options;
    private readonly ConnectionMultiplexer _connection;
    private IDatabase _db => _connection.GetDatabase();

    public RedisCache(IOptions<RedisCacheSettings> options)
    {
        _options = options;
        _connection = GetConnection();
    }

    private ConnectionMultiplexer GetConnection()
    {
        return ConnectionMultiplexer.ConnectAsync(_options.Value.ConnectionString!)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
    }

    public T Set<T>(string key, T item, TimeSpan? expiration = null)
    {
        var ser = JsonSerializer.Serialize(item);
        
        if (expiration is null)
        {
            _db.StringSet(key, ser);
        }

        _db.StringSet(key, ser, expiration);
        
        return item;
    }

    public bool TryGetValue<T>(string key, out T? item)
    {
        var get = _db.StringGet(key);

        if (get.HasValue)
        {
            item = JsonSerializer.Deserialize<T>(get);
            return true;
        }

        item = default;
        return false;
    }
}