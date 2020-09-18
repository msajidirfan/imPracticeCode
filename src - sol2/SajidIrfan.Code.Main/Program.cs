using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SajidIrfan.Code.AppRun;
using SajidIrfan.Code.Helper;
using SajidIrfan.Code.Models;
using SajidIrfan.Code.Services;

namespace SajidIrfan.Code
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // run app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            serviceCollection.AddLogging(); 

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", false)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));
            ConfigureConsole(configuration);

            serviceCollection.AddSingleton<IConsoleWriteHelper, ConsoleWriteHelper>();
            serviceCollection.AddSingleton<IConsoleInputsHelper, ConsoleInputsHelper>();

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();
            serviceCollection.AddTransient<IXMLService, XMLService>();
            serviceCollection.AddTransient<IXMLServiceExtended, XMLServiceExtended>();

            // add app
            serviceCollection.AddTransient<App>();
        }

        private static void ConfigureConsole(IConfigurationRoot configuration)
        {
            System.Console.Title = configuration.GetSection("Configuration:ConsoleTitle").Value;
        }
    }
}
