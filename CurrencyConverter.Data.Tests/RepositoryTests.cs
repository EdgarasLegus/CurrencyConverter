using CurrencyConverter.Data.Repository;
using CurrencyConverter.Data.Settings;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Interfaces.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CurrencyConverter.Data.Tests
{
    public class RepositoryTests
    {
        private IFileReader _fileReaderMock;
        private IOptions<Config> _optionsMock;
        private IRepository _fileRepository;

        [SetUp]
        public void Setup()
        {
            var config = new Config()
            {
                ExchangeRates = "TestExchangeRates.csv"
            };

            _optionsMock = Options.Create(config);
            _fileReaderMock = Substitute.For<IFileReader>();

            _fileRepository = new FileRepository(_optionsMock, _fileReaderMock);
        }

        [Test]
        public void AssembleExchangeRatesList_ShouldSkipDuplicateIsoPairLinesInFile()
        {
            //Arrange
            var testPartsList = new List<List<string>>()
            {
                new List<string>(){ "1", "EUR", "DKK", "7.4357"},
                new List<string>(){ "2", "USD", "GBP", "0.7293"},
                new List<string>(){ "3", "USD", "GBP", "0.4435"},
                new List<string>(){ "4", "NOK", "JPY", "12.8900"},
                new List<string>(){ "5", "JPY", "NOK", "4.4444" }
            };

            var testExchangeRatesList = new List<ExchangeRate>()
            {
                new ExchangeRate(){ID = 1, MainCurrency = "EUR", MoneyCurrency = "DKK", Rate = 7.4357M},
                new ExchangeRate(){ID = 2, MainCurrency = "USD", MoneyCurrency = "GBP", Rate = 0.7293M},
                new ExchangeRate(){ID = 4, MainCurrency = "NOK", MoneyCurrency = "JPY", Rate = 12.8900M}
            };

            _fileReaderMock.GetFile("TestExchangeRates.csv").Returns(testPartsList);

            //Act
            var result = _fileRepository.GetExchangeRates();
            //Assert
            _fileReaderMock.Received(1).GetFile("TestExchangeRates.csv");
            Assert.IsTrue(testExchangeRatesList.Count == result.Count);
        }

        [Test]
        public void AssembleExchangeRatesList_ShouldLoadAllLines()
        {
            //Arrange
            var testPartsList = new List<List<string>>()
            {
                new List<string>(){ "1", "EUR", "DKK", "7.4357"},
                new List<string>(){ "2", "USD", "GBP", "0.7293"},
                new List<string>(){ "3", "NOK", "JPY", "12.8900"},
            };

            var testExchangeRatesList = new List<ExchangeRate>()
            {
                new ExchangeRate(){ID = 1, MainCurrency = "EUR", MoneyCurrency = "DKK", Rate = 7.4357M},
                new ExchangeRate(){ID = 2, MainCurrency = "USD", MoneyCurrency = "GBP", Rate = 0.7293M},
                new ExchangeRate(){ID = 3, MainCurrency = "NOK", MoneyCurrency = "JPY", Rate = 12.8900M}
            };

            _fileReaderMock.GetFile("TestExchangeRates.csv").Returns(testPartsList);

            //Act
            var result = _fileRepository.GetExchangeRates();
            //Assert
            _fileReaderMock.Received(1).GetFile("TestExchangeRates.csv");
            Assert.IsTrue(testExchangeRatesList.Count == result.Count);
        }
    }
}
