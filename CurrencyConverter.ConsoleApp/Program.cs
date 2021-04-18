using CurrencyConverter.BusinessLogic.Services;
using CurrencyConverter.Contracts.Entities;
using CurrencyConverter.Data;
using CurrencyConverter.Data.Repository;
using CurrencyConverter.Data.Settings;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Interfaces.Repository;
using CurrencyConverter.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Resources;

namespace CurrencyConverter.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceBuilder = services.BuildServiceProvider();


            // 1 - get user input
            var userInput = serviceBuilder.GetService<IUserInterface>().GetUserInput();

            // 2 - get created object
            var converter = new Converter().ParseToModel(userInput);

            // 3 - Perform exchange
            var exchangeResult = serviceBuilder.GetService<IExchangeService>().GetExchangeResult(converter.ToDomainObject());

            //4 - Print Result
            serviceBuilder.GetService<IUserInterface>().GetResult(exchangeResult);
           
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            ResourceManager resourceManager = new ResourceManager("CurrencyConverter.ConsoleApp.Resources.UsageDescription", Assembly.GetExecutingAssembly());
            //Config config = new Config();
            //IOptions<Config> options = Options.Create(config);
            //FileReader fileReader = new FileReader();
            //FileRepository fileRepository = new FileRepository(options, fileReader);

            services.AddOptions();
            services.Configure<Config>(configuration.GetSection("Config"));

            services.AddScoped<IExchangeService, ExchangeService>()
                .AddScoped<IFileReader, FileReader>()
                .AddScoped<IRepository, FileRepository>()
                .AddScoped<IUserInterface>(x => new UserInterface(resourceManager));
            return services;
        }
    }
}
