using System;

namespace CurrencyConverter.Domain.Entities
{
    public class Converter
    {
        public string Operation { get; set; }
        public string MainCurrency { get; set; }
        public string MoneyCurrency { get; set; }
        public Decimal Amount { get; set; }
    }
}
