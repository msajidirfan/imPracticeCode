using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SajidIrfan.Code.AppRun;
using SajidIrfan.Code.Helper;
using SajidIrfan.Code.Models;
using SajidIrfan.Code.Services;
using System;
using System.IO;

namespace SajidIrfan.Code.Main.Test
{
    [TestClass]
    public class AppTest
    {

        private readonly IXMLServiceExtended _xmlService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;



        private Mock<IXMLServiceExtended> mockXMLService;
        private Mock<IConsoleInputsHelper> mockConsoleInputsHelper;
        private Mock<IConsoleWriteHelper> mockConsoleWriteHelper;
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
            mockXMLService = new Mock<IXMLServiceExtended>(MockBehavior.Strict);
            mockConsoleInputsHelper = new Mock<IConsoleInputsHelper>(MockBehavior.Strict);
            mockConsoleWriteHelper = new Mock<IConsoleWriteHelper>(MockBehavior.Strict);
            mockLogger = new Mock<ILogger<App>>();
            mockOptions = new Mock<IOptions<AppSettings>>();
            // mockAppSettings = new AppSettings();
            mockOptions.Setup(p => p.Value)
                .Returns(new AppSettings() {  ConsoleTitle = "",   MaxRepeatAllowed = 1, ValidOperationCount = 4, XMLFilePath = "" });
            //mockOptions.Setup(p => p.GetApplicationUser())
            //    .Returns(new SAUserService.Models.UserContext() { SADatabaseId = "", SubscriberUserId = 0 });

            request = new App(mockXMLService.Object, mockConsoleInputsHelper.Object, mockConsoleWriteHelper.Object, mockOptions.Object, mockLogger.Object);
        }

        [TestMethod]
        public void ShouldFail_WhenNullConstructorDependency()
        {
            //Assert// 
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new App(null, mockConsoleInputsHelper.Object, mockConsoleWriteHelper.Object, mockOptions.Object, mockLogger.Object));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));

            ex = Assert.ThrowsException<ArgumentNullException>(() => new App(mockXMLService.Object, null, mockConsoleWriteHelper.Object, mockOptions.Object, mockLogger.Object));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));

            ex = Assert.ThrowsException<ArgumentNullException>(() => new App(mockXMLService.Object, mockConsoleInputsHelper.Object, null, mockOptions.Object, mockLogger.Object));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));

            ex = Assert.ThrowsException<ArgumentNullException>(() => new App(mockXMLService.Object, mockConsoleInputsHelper.Object, mockConsoleWriteHelper.Object, null, mockLogger.Object));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));

            ex = Assert.ThrowsException<ArgumentNullException>(() => new App(mockXMLService.Object, mockConsoleInputsHelper.Object, mockConsoleWriteHelper.Object, mockOptions.Object, null));
            Assert.IsTrue(ex.Message.Contains("Value cannot be null."));
        }

        [TestMethod]
        public void ShouldRun_WhenUpdateEmployee()
        {
            //Arrange
            string name = "AAA";
            mockConsoleInputsHelper.Setup(p => p.GetDetails<int>())
                .Returns("3");
            mockConsoleInputsHelper.SetupSequence(p => p.GetDetails<string>())
                .Returns("Name")
                .Returns("E");
            mockConsoleInputsHelper.Setup(p => p.ClosingHolder()).Verifiable();

            mockConsoleWriteHelper.Setup(p => p.ConsoleInitialMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.UpdateMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.NextOperationMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.ClosingThanksMessage()).Verifiable();

            mockXMLService.Setup(p => p.UpdateEmployee(It.IsAny<string>()))
                .Returns(true);

            //Act
            request.Run();

            //Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRun_ReadAllEmployee_WhenValidData()
        {
            //Arrange
            mockConsoleInputsHelper.Setup(p => p.GetDetails<int>())
                .Returns("1");
            mockConsoleInputsHelper.SetupSequence(p => p.GetDetails<string>())
                .Returns("E");
            mockConsoleInputsHelper.Setup(p => p.ClosingHolder()).Verifiable();

            mockConsoleWriteHelper.Setup(p => p.ConsoleInitialMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.ReadAllMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.NextOperationMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.ClosingThanksMessage()).Verifiable();

            mockXMLService.Setup(p => p.ReadAllEmployees())
                .Returns(true);

            //Act
            request.Run();

            //Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRun_AddEmployee_WhenValidData()
        {
            //Arrange
            mockConsoleInputsHelper.Setup(p => p.GetDetails<int>())
                .Returns("2");
            mockConsoleInputsHelper.SetupSequence(p => p.GetDetails<string>())
                .Returns("E");
            mockConsoleInputsHelper.Setup(p => p.ClosingHolder()).Verifiable();

            mockConsoleWriteHelper.Setup(p => p.ConsoleInitialMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.AddMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.NextOperationMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.ClosingThanksMessage()).Verifiable();

            mockXMLService.Setup(p => p.AddEmployee())
                .Returns(true);

            //Act
            request.Run();

            //Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRun_DeleteEmployee_WhenValidData()
        {
            //Arrange
            string name = "AAA";
            mockConsoleInputsHelper.Setup(p => p.GetDetails<int>())
                .Returns("4");
            mockConsoleInputsHelper.SetupSequence(p => p.GetDetails<string>())
                .Returns(name)
                .Returns("E");
            mockConsoleInputsHelper.Setup(p => p.ClosingHolder()).Verifiable();

            mockConsoleWriteHelper.Setup(p => p.ConsoleInitialMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.DeleteMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.NextOperationMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.ClosingThanksMessage()).Verifiable();

            mockXMLService.Setup(p => p.DeleteEmployee(It.IsAny<string>()))
                .Returns(true);

            //Act
            request.Run();

            //Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldRun_Default_WhenInvalidInputOperation()
        {
            //Arrange
            mockConsoleInputsHelper.Setup(p => p.GetDetails<int>())
                .Returns("5");
            mockConsoleInputsHelper.SetupSequence(p => p.GetDetails<string>())
                .Returns("5")
                .Returns("E");
            mockConsoleInputsHelper.Setup(p => p.ClosingHolder()).Verifiable();

            mockConsoleWriteHelper.Setup(p => p.ConsoleInitialMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.NextOperationMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.ClosingThanksMessage()).Verifiable();
            mockConsoleWriteHelper.Setup(p => p.InvalidOperationMessage()).Verifiable();


            //Act
            request.Run();

            //Assert
            //Assert.IsNotNull(result);
        }

    }
}
