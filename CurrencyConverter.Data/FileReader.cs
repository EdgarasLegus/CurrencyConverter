using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CurrencyConverter.Interfaces;

namespace CurrencyConverter.Data
{
    public class FileReader : IFileReader
    {
        public List<List<string>> GetFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception($"Data file {fileName} does not exist!");
            }

            var partsList = new List<List<string>>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',').Select(p => p.Trim()).ToList();
                    partsList.Add(parts);
                }
            }
            return partsList;
        }
    }
}
