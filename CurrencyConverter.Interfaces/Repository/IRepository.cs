using CurrencyConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Interfaces.Repository
{
    public interface IRepository
    {
        List<ExchangeRate> GetExchangeRates();
    }
}
