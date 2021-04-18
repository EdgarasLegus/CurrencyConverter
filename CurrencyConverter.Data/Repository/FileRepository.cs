using CurrencyConverter.Data.Settings;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Interfaces.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyConverter.Data.Repository
{
    public class FileRepository : IRepository
    {
        private readonly Config _options;
        private readonly IFileReader _fileReader;
        public FileRepository(IOptions<Config> options, IFileReader fileReader)
        {
            _options = options.Value;
            _fileReader = fileReader;
        }

        public List<ExchangeRate> GetExchangeRates()
        {
            var fileName = _options.ExchangeRates;
            var fileParts = _fileReader.GetFile(fileName);
            var exchangeRatesList = new List<ExchangeRate>();

            foreach(List<string> sublist in fileParts)
            {
                var exchangeRate = new ExchangeRate()
                {
                    ID = Int32.Parse(sublist[0]),
                    MainCurrency = sublist[1],
                    MoneyCurrency = sublist[2],
                    Rate = Convert.ToDecimal(sublist[3])
                };
                if (!IsDuplicate(exchangeRatesList, exchangeRate))
                {
                    exchangeRatesList.Add(exchangeRate);
                }
            }
            return exchangeRatesList;
        }

        private bool IsDuplicate(List<ExchangeRate> exchangeRatesList, ExchangeRate exchangeRate)
        {
            return exchangeRatesList.Any(x => x.MainCurrency == exchangeRate.MainCurrency &&
            x.MoneyCurrency == exchangeRate.MoneyCurrency);
        }
    }
}
