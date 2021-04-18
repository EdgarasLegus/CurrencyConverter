using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Interfaces
{
    public interface IFileReader
    {
        List<List<string>> GetFile(string fileName);
    }
}
