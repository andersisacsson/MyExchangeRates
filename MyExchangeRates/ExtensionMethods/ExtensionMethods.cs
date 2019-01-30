using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExchangeRates.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string ToDateString(this int datepart)
        {
            return datepart >= 10 ? datepart.ToString() : "0" + datepart;
        }
    }
}
