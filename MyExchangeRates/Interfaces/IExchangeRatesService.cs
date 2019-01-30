using MyExchangeRates.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExchangeRates.Interfaces
{
    public interface IExchangeRatesService
    {
        Task<ExchangeRateJson> GetExchangeRates(IEnumerable<DateTime> dates, string baseCurrency, string targetCurrency);
    }
}