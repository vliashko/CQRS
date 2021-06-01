using CQRS.Domain.ForeignExchange;
using CQRS.Infrastructure.Caching;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace CQRS.Infrastructure.Domain.ForeignExchanges
{
    public class ForeignExchange : IForeignExchange
    {
        private readonly ICacheStore _cacheStore;

        public ForeignExchange(ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        public List<ConversionRate> GetConversionRates()
        {
            var ratesCache = _cacheStore.Get(new ConversionRatesCacheKey());

            if (ratesCache != null)
            {
                return ratesCache.Rates;
            }

            List<ConversionRate> rates = GetConversionRatesFromExternalApi();

            _cacheStore.Add(new ConversionRatesCache(rates), new ConversionRatesCacheKey(), DateTime.Now.Date.AddDays(1));

            return rates;
        }

        private static List<ConversionRate> GetConversionRatesFromExternalApi()
        {
            WebClient client = new WebClient();
            string result1 = client.DownloadString("https://api.exchangerate.host/latest?base=USD&symbols=EUR");
            string result2 = client.DownloadString("https://api.exchangerate.host/latest?base=EUR&symbols=USD");
            dynamic result = JsonConvert.DeserializeObject(result1);
            string eur = result.rates.EUR;
            result = JsonConvert.DeserializeObject(result2);
            string usd = result.rates.USD;

            var conversionRates = new List<ConversionRate>
            {
                new ConversionRate("USD", "EUR", decimal.Parse(eur, CultureInfo.InvariantCulture)),
                new ConversionRate("EUR", "USD", decimal.Parse(usd, CultureInfo.InvariantCulture))
            };

            return conversionRates;
        }
    }
}