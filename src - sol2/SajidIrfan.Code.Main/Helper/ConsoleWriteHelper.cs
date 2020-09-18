using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SajidIrfan.Code.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SajidIrfan.Code.Helper
{
    public class ConsoleWriteHelper : IConsoleWriteHelper
    {
        private readonly ILogger<ConsoleWriteHelper> _logger;
        private readonly AppSettings _config;
        public ConsoleWriteHelper(IOptions<AppSettings> config,
            ILogger<ConsoleWriteHelper> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Writes Initial Message to console.
        /// </summary>
        public void ConsoleInitialMessage()
        {
            System.Console.WriteLine($"This is a console application for {_config.ConsoleTitle}");
            System.Console.WriteLine($"Choose option from below to perform the operation and Press Enter.");
            System.Console.WriteLine($"1 - Read the XML file.");
            System.Console.WriteLine($"2 - Add new employee.");
            System.Console.WriteLine($"3 - Update an existing employee.");
            System.Console.WriteLine($"4 - Delete existing employee.");
        }

        /// <summary>
        /// Print Delete Message
        /// </summary>
        public void DeleteMessage()
        {
            System.Console.WriteLine("Delete operation started. \n Enter the Employee Name to be deleted. Should be a Complete match.");
        }

        /// <summary>
        /// Print Add Message
        /// </summary>
        public void AddMessage()
        {
            System.Console.WriteLine("Add Employee.");
        }

        /// <summary>
        /// Print Update Message
        /// </summary>
        public void UpdateMessage()
        {
            System.Console.WriteLine("Update operation started. Enter the Employee Name to be updated. Should be a Complete match.");
        }

        /// <summary>
        /// Print Read All Message
        /// </summary>
        public void ReadAllMessage()
        {
            Console.WriteLine("Reading the XML data: Started.");
        }

        /// <summary>
        /// Print Next Operation Message
        /// </summary>
        public void NextOperationMessage()
        {
            System.Console.WriteLine("Press R, to repeat the same operation. " +
                                "\n For switching operation, Press respective operation number.\n " +
                                "Press E to exit if earlier operation was successful");
        }

        /// <summary>
        /// Print Invalid Operation Message
        /// </summary>
        public void InvalidOperationMessage()
        {
            System.Console.WriteLine("Invalid input. Please retry");
        }

        /// <summary>
        /// Print Closing Thanks Message
        /// </summary>
        public void ClosingThanksMessage()
        {
            System.Console.WriteLine("Thank you. Exiting now. To increase retries change value in appsettings.json");
        }
    }
}
