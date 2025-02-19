using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace forms.Services;
public class RedisService : IDisposable
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisService(string redisConnection)
    {
        _redis = ConnectionMultiplexer.Connect(redisConnection);
        _db = _redis.GetDatabase();
    }

    public IDatabase GetDatabase() => _db;

    public SearchCommands GetSearch() => _db.FT();

    public void Dispose()
    {
        _redis?.Dispose();
    }
}
