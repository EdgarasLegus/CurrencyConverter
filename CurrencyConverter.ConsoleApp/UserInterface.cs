using CurrencyConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace CurrencyConverter.ConsoleApp
{
    public class UserInterface : IUserInterface
    {
        private readonly ResourceManager _resourceManager;
        public UserInterface(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string[] GetUserInput()
        {
            var operation = "Exchange";
            string[] userInput;
            do
            {
                Console.WriteLine($"Enter the function and parameters ({operation}): ");
                userInput = Console.ReadLine().Split(' ');
                if (userInput.Length != 3)
                {
                    Console.WriteLine("Incorrect usage, try again: " + "\n" + _resourceManager.GetString("Exchange"));
                }
            }
            while (!(userInput.Length == 3 && userInput[0] == operation));
                return userInput;
        }

        public void GetResult(Decimal? exchangeResult)
        {
            if (exchangeResult == null)
            {
                Console.WriteLine("One of the currencies is not found!");
            }
            else
            {
                Console.WriteLine(exchangeResult);
            }
        }
    }
}
