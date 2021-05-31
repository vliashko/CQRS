using CQRS.Infrastructure.Caching;

namespace CQRS.Infrastructure.Domain.ForeignExchanges
{
    public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
    {
        public string CacheKey => "ConversionRatesCache";
    }
}