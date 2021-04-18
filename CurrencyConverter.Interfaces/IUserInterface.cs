using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Interfaces
{
    public interface IUserInterface
    {
        string[] GetUserInput();
        void GetResult(Decimal? result);
    }
}
