using CurrencyConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Interfaces.Services
{
    public interface IExchangeService
    {
        Decimal? GetExchangeResult(Converter converter);
    }
}
