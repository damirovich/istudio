﻿namespace ISTUDIO.Application.Common.Interfaces;

public interface IRedisCacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
}
