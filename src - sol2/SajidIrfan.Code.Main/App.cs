using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SajidIrfan.Code.Helper;
using SajidIrfan.Code.Models;
using SajidIrfan.Code.Services;
using System;

namespace SajidIrfan.Code
{
    public class App
    {
        private readonly IXMLService _xmlService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        public App(IXMLService xmlService,
            IOptions<AppSettings> config,
            ILogger<App> logger)
        {
            _xmlService = xmlService ?? throw new ArgumentNullException(nameof(xmlService)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config)); 
        }

        /// <summary>
        /// This method controls the flow. All Employee CRUD happens here.
        /// </summary>
        public void Run()
        {
            System.Console.WriteLine($"This is a console application for {_config.ConsoleTitle}");
            System.Console.WriteLine($"Choose option from below to perform the operation and Press Enter.");
            System.Console.WriteLine($"1 - Read the XML file.");
            System.Console.WriteLine($"2 - Add new employee.");
            System.Console.WriteLine($"3 - Update an existing employee.");
            System.Console.WriteLine($"4 - Delete existing employee.");

            bool retry = true; int counter = 0; string empName = "";
            int op = 0;
            op = Int32.Parse(GetInputsFromConsole.GetDetails<int>());
            while (retry && counter < _config.MaxRepeatAllowed)
            {
                switch (op)
                {
                    case 1: //ReadAll
                        System.Console.WriteLine("Reading the XML data: Started.");
                        retry = !_xmlService.ReadAllEmployees();
                        break;
                    case 3: // Update
                        System.Console.WriteLine("Update operation started. Enter the Employee Name to be updated. Should be a Complete match.");
                        empName = GetInputsFromConsole.GetDetails<string>();
                        retry = !_xmlService.UpdateEmployee(empName);
                        break;
                    case 2: // Add
                        System.Console.WriteLine("Add Employee.");
                        retry = !_xmlService.AddEmployee();
                        break;
                    case 4: // Delete
                        System.Console.WriteLine("Delete operation started. \n Enter the Employee Name to be deleted. Should be a Complete match.");
                        empName = GetInputsFromConsole.GetDetails<string>();
                        retry = !_xmlService.DeleteEmployee(empName);
                        break;
                    default:
                        break;
                }
                System.Console.WriteLine("{0} repeats left. Press R, to repeat the same operation. " +
                    "\n For switching operation, Press respective operation number.\n " +
                    "Press Enter to exit if earlier operation was successful", _config.MaxRepeatAllowed - counter - 1);
                string repeatInput = GetInputsFromConsole.GetDetails<string>();
                int parsedResult = 0;
                Int32.TryParse(repeatInput, out parsedResult);
                if (repeatInput.ToLower() == "r")
                {
                    retry = true;
                }
                else if (parsedResult > 0 && parsedResult <= _config.ValidOperationCount)
                {
                    op = Int32.Parse(repeatInput);
                    retry = true;
                }
                counter++;
            }

            System.Console.WriteLine($"Thank you. Exiting now. To increase retries change value in appsettings.json");
            System.Console.ReadKey();
        }

        /// <summary>
        /// Reads data from console
        /// </summary>
        /// <param name="op">input</param>
        /// <returns>user typed data</returns>
        private int GetOperations(int op)
        {
            try
            {
                var input = Convert.ToInt32(System.Console.ReadLine());
                if (input > 0 && input < _config.ValidOperationCount)
                {
                    op = input;
                    _logger.LogInformation($"Valid input");
                }
                else
                {
                    System.Console.WriteLine($"Invalid number. Please retry with valid number.");
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"Invalid input. Please retry with valid number.");
            }

            return op;
        }
    }
}
