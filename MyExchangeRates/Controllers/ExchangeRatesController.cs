using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExchangeRates.Interfaces;

namespace MyExchangeRates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService;

        public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        public async Task<IActionResult> Get(string dates, string baseCurrency, string targetCurrency)
        {
            try
            {
                var dateList = dates.Split(',').Select(CreateDateTime).ToList();
                var result = await _exchangeRatesService.GetExchangeRates(dateList, baseCurrency, targetCurrency);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private DateTime CreateDateTime(string date)
        {
            var dateArr = date.Split('-');
            return new DateTime(Convert.ToInt32(dateArr[0]), Convert.ToInt32(dateArr[1]), Convert.ToInt32(dateArr[2]));
        }


    }
}
