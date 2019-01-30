using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MyExchangeRates.ExtensionMethods;
using MyExchangeRates.Interfaces;
using MyExchangeRates.Models;

namespace MyExchangeRates.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private static readonly HttpClient _client;

        static ExchangeRatesService()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://api.exchangeratesapi.io/") };
        }

        public async Task<ExchangeRateJson> GetExchangeRates(IEnumerable<DateTime> dates, string baseCurrency, string targetCurrency)
        {
            var minDate = dates.Min();
            var maxDate = dates.Max();
            var startAt = $"{minDate.Year}-{minDate.Month.ToDateString()}-{minDate.Day.ToDateString()}";
            var endAt = $"{maxDate.Year}-{maxDate.Month.ToDateString()}-{maxDate.Day.ToDateString()}";

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _client.GetAsync($"history?start_at={startAt}&end_at={endAt}&base={baseCurrency}&symbols={targetCurrency}");

            return (response.IsSuccessStatusCode) ? await GetExchangeRateJson(response, dates) : null;
        }

        private async Task<ExchangeRateJson> GetExchangeRateJson(HttpResponseMessage response, IEnumerable<DateTime> dates)
        {
            var allExchangeRatesInPeriod = await response.Content.ReadAsAsync<ExchangeRate>();
            var requestedExchangeRates = allExchangeRatesInPeriod.Rates.Where(x => dates.Any(a => a == x.Key)).ToList();
            var minRate = requestedExchangeRates.OrderBy(x => x.Value.Values.First()).First();
            var maxRate = requestedExchangeRates.OrderByDescending(x => x.Value.Values.First()).First();

            return new ExchangeRateJson
            {
                MinRateValue = minRate.Value.Values.First(),
                MinRateDate = minRate.Key,
                MaxRateValue = maxRate.Value.Values.First(),
                MaxRateDate = maxRate.Key,
                AvgRate = requestedExchangeRates.Average(x => x.Value.Values.First())
            };
        }
    }
}
