using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartHotel.Abstraction;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHotel.Infrastructure.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICachableQuery cacheableQuery)
            {
                TResponse response;
                if (cacheableQuery.BypassCache) return await next();
                async Task<TResponse> GetResponseAndAddToCache()
                {
                    response = await next();
                    var slidingExpiration =  cacheableQuery.SlidingExpiration;
                    var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                    var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                    await _cache.SetAsync(cacheableQuery.CacheKey, serializedData, options, cancellationToken);
                    return response;
                }

                var cachedResponse = await _cache.GetAsync(cacheableQuery.CacheKey, cancellationToken);
                if (cachedResponse != null)
                {
                    response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
                    _logger.LogInformation($"Fetched from Cache -> '{cacheableQuery.CacheKey}'.");
                }
                else
                {
                    response = await GetResponseAndAddToCache();
                    _logger.LogInformation($"Added to Cache -> '{cacheableQuery.CacheKey}'.");
                }

                return response;
            }
            else
            {
                return await next();
            }
        }
    }
}
