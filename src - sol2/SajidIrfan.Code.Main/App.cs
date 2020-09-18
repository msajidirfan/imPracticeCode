using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SajidIrfan.Code.Helper;
using SajidIrfan.Code.Main.Helper.Enum;
using SajidIrfan.Code.Models;
using SajidIrfan.Code.Services;
using System;

namespace SajidIrfan.Code.AppRun
{
    public class App
    {
        private readonly IConsoleInputsHelper _consoleInputsHelper;
        private readonly IConsoleWriteHelper _consoleWriteHelper;
        private readonly IXMLServiceExtended _xmlService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        public App(IXMLServiceExtended xmlService, IConsoleInputsHelper consoleInputsHelper, IConsoleWriteHelper consoleWriteHelper,
            IOptions<AppSettings> config,
            ILogger<App> logger)
        {
            _consoleInputsHelper = consoleInputsHelper ?? throw new ArgumentNullException(nameof(consoleInputsHelper));
            _consoleWriteHelper = consoleWriteHelper ?? throw new ArgumentNullException(nameof(consoleWriteHelper));
            _xmlService = xmlService ?? throw new ArgumentNullException(nameof(xmlService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// This method controls the flow. All Employee CRUD happens here.
        /// </summary>
        public void Run()
        {
            _consoleWriteHelper.ConsoleInitialMessage();

            bool retry = true; int counter = 0; string empName = "";
            int op = 0;
            op = Int32.Parse(_consoleInputsHelper.GetDetails<int>());
            // while (retry && counter < _config.MaxRepeatAllowed)
            while (retry)
            {
                switch (op)
                {
                    case (int)Operations.ReadAll: //ReadAll
                        _consoleWriteHelper.ReadAllMessage();
                        retry = !_xmlService.ReadAllEmployees();
                        break;
                    case (int)Operations.Add: // Add
                        _consoleWriteHelper.AddMessage();
                        retry = !_xmlService.AddEmployee();
                        break;
                    case (int)Operations.Update: // Update
                        _consoleWriteHelper.UpdateMessage();
                        empName = _consoleInputsHelper.GetDetails<string>();
                        retry = !_xmlService.UpdateEmployee(empName);
                        break;
                    case (int)Operations.Delete: // Delete
                        _consoleWriteHelper.DeleteMessage();
                        empName = _consoleInputsHelper.GetDetails<string>();
                        retry = !_xmlService.DeleteEmployee(empName);
                        break;
                    default:
                        break;
                }
                _consoleWriteHelper.NextOperationMessage();
                string repeatInput = _consoleInputsHelper.GetDetails<string>();
                int parsedResult = 0;
                Int32.TryParse(repeatInput, out parsedResult);
                if (repeatInput.ToLower() == "r")
                {
                    retry = true;
                }
                else if (repeatInput.ToLower() == "e")
                {
                    retry = false;
                }
                else if (parsedResult > 0 && parsedResult <= _config.ValidOperationCount)
                {
                    op = Int32.Parse(repeatInput);
                    retry = true;
                }
                else
                {
                    op = 0;
                    retry = true;
                    _consoleWriteHelper.InvalidOperationMessage();
                }
                //counter++;
            }

            _consoleWriteHelper.ClosingThanksMessage();
            _consoleInputsHelper.ClosingHolder();
        }
    }
}
