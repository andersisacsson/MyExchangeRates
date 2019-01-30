using System;
using Newtonsoft.Json;

namespace MyExchangeRates.Models
{
    public class ExchangeRateJson
    {
        [JsonProperty]
        public decimal MinRateValue { set; get; }
        [JsonProperty]
        public DateTime MinRateDate { set; get; }

        [JsonProperty]
        public decimal MaxRateValue { set; get; }
        [JsonProperty]
        public DateTime MaxRateDate { set; get; }

        [JsonProperty]
        public decimal AvgRate { set; get; }
    }
}