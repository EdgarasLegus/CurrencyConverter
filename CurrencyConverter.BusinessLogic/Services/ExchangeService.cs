using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Interfaces.Repository;
using CurrencyConverter.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.BusinessLogic.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IRepository _repository;
        public ExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public Decimal? GetExchangeResult(Converter converter)
        {
            var exchangeRates = _repository.GetExchangeRates();

            if (converter.MainCurrency
                == converter.MoneyCurrency)
            {
                return converter.Amount;
            }
            else
            {
                return converter.MainCurrency
                    != converter.MoneyCurrency ? MakeExchange(exchangeRates, converter) : null;
            }
        }

        private Decimal? MakeExchange(List<ExchangeRate> exchangeRates, Converter converter)
        {
            var exchangeRatesLine = exchangeRates.Where(line => (line.MainCurrency == converter.MainCurrency && line.MoneyCurrency == converter.MoneyCurrency)
                || (line.MainCurrency == converter.MoneyCurrency && line.MoneyCurrency == converter.MainCurrency)).FirstOrDefault();

            return exchangeRatesLine == null
                ? null
                : (decimal?)(exchangeRatesLine.MainCurrency == converter.MainCurrency
                ? Math.Round(converter.Amount * exchangeRatesLine.Rate, 2)
                : Math.Round(converter.Amount * 1 / exchangeRatesLine.Rate, 2));

        }
    }
}
