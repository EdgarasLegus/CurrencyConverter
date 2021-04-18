using CurrencyConverter.Data.Settings;
using CurrencyConverter.Interfaces;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Data.Tests
{
    public class FileReaderTests
    {
        private IFileReader _fileReader;
        private IOptions<Config> _optionsMock;

        [SetUp]
        public void Setup()
        {
            var config = new Config()
            {
                ExchangeRates = "TestExchangeRates.csv"
            };

            _optionsMock = Options.Create(config);

            _fileReader = new FileReader();
        }

        [Test]
        public void FileIsFound_ShouldExist()
        {
            //Arrange
            var fileName = "TestExchangeRates.csv";

            //Act, Assert
            FileAssert.Exists(fileName);
        }

        [Test]
        public void FileIsNotFound_ShouldThrowException()
        {
            //Arrange
            var fileName = "Rates.txt";

            //Act, Assert
            Assert.Throws<Exception>(() => _fileReader.GetFile(fileName));
        }

        [Test]
        public void AssembleLines_ShouldReturnListofLines()
        {
            //Arrange
            var fileName = "TestExchangeRates.csv";

            var testPartsList = new List<List<string>>()
            {
                new List<string>(){ "1", "EUR", "DKK", "7.4357"},
                new List<string>(){ "2", "USD", "GBP", "0.7293"},
                new List<string>(){ "3", "USD", "GBP", "0.4435"},
                new List<string>(){ "4", "NOK", "JPY", "12.8900"},
                new List<string>(){ "5", "JPY", "NOK", "4.4444" }
            };

            //Act
            var result = _fileReader.GetFile(fileName);

            //Assert
            Assert.IsTrue(result.Count == testPartsList.Count);
        }
    }
}
