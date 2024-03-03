
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using Vegefoods.Application.Common.Caching;

namespace Vegefoods.Persistence.Common.Caching
{			   	
	public class CacheService : ICacheService
	{
		private readonly IMemoryCache _memoryCache;
		private readonly CacheOptions _cacheOptions;
		public CacheService(IMemoryCache memoryCache, CacheOptions cacheOptions)
		{
			_memoryCache = memoryCache;
			_cacheOptions = cacheOptions;
		}
		public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, TimeSpan? timeSpan = null) where T : class
		{
			if (!_memoryCache.TryGetValue(cacheKey, out T result))
			{
				result = await createItem();
				var cacheEntryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = timeSpan ?? TimeSpan.FromSeconds(_cacheOptions.ExpireSecond)
				};

				_memoryCache.Set(cacheKey, result, cacheEntryOptions);
			}

			return result;
		}
	}
}
