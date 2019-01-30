using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExchangeRates.Models
{
    public class ExchangeRate
    {
        public Dictionary<DateTime, Dictionary<string, decimal>> Rates { get; set; }
    }
}
