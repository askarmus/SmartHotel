using System;

namespace SmartHotel.Exceptions.Abstraction
{
    public interface ICachableQuery
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}
