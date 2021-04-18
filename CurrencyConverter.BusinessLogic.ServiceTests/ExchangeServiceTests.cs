using CurrencyConverter.BusinessLogic.Services;
using CurrencyConverter.Data.Repository;
using CurrencyConverter.Data.Settings;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Interfaces.Repository;
using CurrencyConverter.Interfaces.Services;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CurrencyConverter.BusinessLogic.ServiceTests
{
    public class ExchangeServiceTests
    {
        private IRepository _repositoryMock;
        private IExchangeService _exchangeService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = Substitute.For<IRepository>();
            _exchangeService = new ExchangeService(_repositoryMock);
        }

        [Test]
        public void IsoCurrencyPairIsSame_ShouldReturnSameAmount()
        {
            //Arrange
            var testConverter = new Converter()
            {
                Operation = "Exchange",
                MainCurrency = "NOK",
                MoneyCurrency = "NOK",
                Amount = 400
            };

            var testExchangeRatesList = new List<ExchangeRate>()
            {
                new ExchangeRate(){ID = 1, MainCurrency = "EUR", MoneyCurrency = "DKK", Rate = 7.4357M},
                new ExchangeRate(){ID = 2, MainCurrency = "USD", MoneyCurrency = "GBP", Rate = 0.7293M},
                new ExchangeRate(){ID = 3, MainCurrency = "NOK", MoneyCurrency = "JPY", Rate = 12.8900M}
            };

            _repositoryMock.GetExchangeRates().Returns(testExchangeRatesList);

            //Act
            var testExchangeResult = _exchangeService.GetExchangeResult(testConverter);

            //Assert
            _repositoryMock.Received(1).GetExchangeRates();

            Assert.AreEqual(testConverter.Amount, testExchangeResult);
        }

        [Test]
        public void IsoCurrencyPairIsDifferent_ShouldMakeExchange()
        {
            //Arrange
            var testConverter = new Converter()
            {
                Operation = "Exchange",
                MainCurrency = "EUR",
                MoneyCurrency = "DKK",
                Amount = 400
            };

            var testExchangeRatesList = new List<ExchangeRate>()
            {
                new ExchangeRate(){ID = 1, MainCurrency = "EUR", MoneyCurrency = "DKK", Rate = 7.4357M},
                new ExchangeRate(){ID = 2, MainCurrency = "USD", MoneyCurrency = "GBP", Rate = 0.7293M},
                new ExchangeRate(){ID = 3, MainCurrency = "NOK", MoneyCurrency = "JPY", Rate = 12.8900M}
            };

            var expectedResult = Math.Round(testConverter.Amount * testExchangeRatesList[0].Rate, 2);

            _repositoryMock.GetExchangeRates().Returns(testExchangeRatesList);

            //Act
            var testExchangeResult = _exchangeService.GetExchangeResult(testConverter);

            //Assert
            _repositoryMock.Received(1).GetExchangeRates();

            Assert.AreEqual(expectedResult, testExchangeResult);
        }

        [Test]
        public void IsoCurrencyPairIsDifferent_ShouldMakeOppositeExchange()
        {
            //Arrange
            var testConverter = new Converter()
            {
                Operation = "Exchange",
                MainCurrency = "GBP",
                MoneyCurrency = "USD",
                Amount = 35
            };

            var testExchangeRatesList = new List<ExchangeRate>()
            {
                new ExchangeRate(){ID = 1, MainCurrency = "EUR", MoneyCurrency = "DKK", Rate = 7.4357M},
                new ExchangeRate(){ID = 2, MainCurrency = "USD", MoneyCurrency = "GBP", Rate = 0.7293M},
                new ExchangeRate(){ID = 3, MainCurrency = "NOK", MoneyCurrency = "JPY", Rate = 12.8900M}
            };

            var expectedResult = Math.Round(testConverter.Amount *  1 / testExchangeRatesList[1].Rate, 2);

            _repositoryMock.GetExchangeRates().Returns(testExchangeRatesList);

            //Act
            var testExchangeResult = _exchangeService.GetExchangeResult(testConverter);

            //Assert
            _repositoryMock.Received(1).GetExchangeRates();

            Assert.AreEqual(expectedResult, testExchangeResult);
        }

        [Test]
        public void UnknownCurrencyInPair_ShouldReturnNull()
        {
            //Arrange
            var testConverter = new Converter()
            {
                Operation = "Exchange",
                MainCurrency = "CCC",
                MoneyCurrency = "USD",
                Amount = 35
            };

            var testExchangeRatesList = new List<ExchangeRate>()
            {
                new ExchangeRate(){ID = 1, MainCurrency = "EUR", MoneyCurrency = "DKK", Rate = 7.4357M},
                new ExchangeRate(){ID = 2, MainCurrency = "USD", MoneyCurrency = "GBP", Rate = 0.7293M},
                new ExchangeRate(){ID = 3, MainCurrency = "NOK", MoneyCurrency = "JPY", Rate = 12.8900M}
            };

            Decimal? expectedResult = null;

            _repositoryMock.GetExchangeRates().Returns(testExchangeRatesList);

            //Act
            var testExchangeResult = _exchangeService.GetExchangeResult(testConverter);

            //Assert
            _repositoryMock.Received(1).GetExchangeRates();

            Assert.AreEqual(expectedResult, testExchangeResult);
        }
    }
}
