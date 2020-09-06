using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SajidIrfan.Code.Models;
using SajidIrfan.Code.Services;
using System;
using System.IO;

namespace SajidIrfan.Code.Main.Test
{
    [TestClass]
    public class AppTest
    {

        private readonly IXMLService _xmlService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;



        private Mock<IXMLService> mockXMLService;
        private Mock<ILogger<App>> mockLogger;
        private AppSettings mockAppSettings;
        private Mock<IOptions<AppSettings>> mockOptions;
        private App request;
        [TestInitialize]
        public void TestInitialize()
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("app-settings.test.json", false)
            //    .Build();
            //mockAppSettings = configuration.Get<AppSettings>(); 
            mockXMLService = new Mock<IXMLService>(MockBehavior.Strict);
            mockLogger = new Mock<ILogger<App>>();
            mockOptions = new Mock<IOptions<AppSettings>>();
            // mockAppSettings = new AppSettings();
            mockOptions.Setup(p => p.Value)
                .Returns(new AppSettings() {  ConsoleTitle = "",   MaxRepeatAllowed = 1, ValidOperationCount = 4, XMLFilePath = "" });
            //mockOptions.Setup(p => p.GetApplicationUser())
            //    .Returns(new SAUserService.Models.UserContext() { SADatabaseId = "", SubscriberUserId = 0 });

            request = new App(mockXMLService.Object, mockOptions.Object, mockLogger.Object);
        }

        [TestMethod]
        public void ShouldFail_WhenNullConstructorDependency()
        {
            //Assert// 
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new App(null, mockOptions.Object, mockLogger.Object));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));

            ex = Assert.ThrowsException<ArgumentNullException>(() => new App(mockXMLService.Object, null, mockLogger.Object));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));

            ex = Assert.ThrowsException<ArgumentNullException>(() => new App(mockXMLService.Object, mockOptions.Object, null));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));
        }

        [TestMethod]
        public void ShouldRun_WhenUpdateEmployee()
        {
            //Arrange
            string name = "AAA";
            mockXMLService.Setup(p => p.UpdateEmployee(It.IsAny<string>()))
                .Returns(true);

            //Act
            request.Run();

            //Assert
            //Assert.IsNotNull(result);
        }

    }
}
