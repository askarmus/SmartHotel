using System;

namespace SmartHotel.Abstraction
{
    public interface ICachableQuery
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}
