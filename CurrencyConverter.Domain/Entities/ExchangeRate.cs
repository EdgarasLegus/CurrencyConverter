using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Domain.Entities
{
    public class ExchangeRate
    {
        public int ID { get; set; }
        public string MainCurrency { get; set; }
        public string MoneyCurrency { get; set; }
        public Decimal Rate { get; set; }
    }
}
