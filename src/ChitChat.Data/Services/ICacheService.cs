namespace ChitChat.Data.Services;

public interface ICacheService
{
    Task<string> GetCachValueAsync(string key);
    Task SetCachValueAsync(string key, string value);
    List<string> GetAllKeys();
}
