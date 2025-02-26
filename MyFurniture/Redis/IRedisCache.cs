namespace MyFurniture.Redis;

public interface IRedisCache
{
    public T Set<T>(string key, T item, TimeSpan? expiration = null);
    public bool TryGetValue<T>(string key, out T? item);
}