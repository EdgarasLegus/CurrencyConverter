using System;
using System.Linq;

namespace CurrencyConverter.Contracts.Entities
{
    public class Converter
    {
        public string Operation { get; set; }
        public string MainCurrency { get; set; }
        public string MoneyCurrency { get; set; }
        public Decimal Amount { get; set; }

        //public Converter(Data.Converter converterDomain)
        //{
        //    Operation = converterDomain.Operation;
        //    MainCurrency = converterDomain.MainCurrency;
        //    MoneyCurrency = converterDomain.MoneyCurrency;
        //    Amount = converterDomain.Amount;
        //}
        public Converter ParseToModel(string[] userInput)
        {
            //var parts = userInput.Split(' ').Select(p => p.Trim()).ToList();
            var isoCurrency = userInput[1].Split('/').Select(ic => ic.Trim()).ToList();
            return new Converter()
            {
                Operation = userInput[0],
                MainCurrency = isoCurrency[0],
                MoneyCurrency = isoCurrency[1],
                Amount = Convert.ToDecimal(userInput[2])

            };
        }

        public Domain.Entities.Converter ToDomainObject()
        {
            return new Domain.Entities.Converter
            {
                Operation = Operation,
                MainCurrency = MainCurrency,
                MoneyCurrency = MoneyCurrency,
                Amount = Amount
            };
        }
    }
}
