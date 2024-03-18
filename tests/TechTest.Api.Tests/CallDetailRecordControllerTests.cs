using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text;
using TechTest.Api.Controllers;
using TechTest.Business.Interfaces;
using TechTest.Business.Services;

namespace TechTest.Api.Tests
{
    public class CallDetailRecordControllerTests
    {
        private readonly Mock<ICallDetailRecordService> service=new();
        private readonly Mock<IConfiguration> configuration = new();
        private readonly Mock<INotifier> notifier = new();
        private CallDetailRecordController controller;
        private Fixture fixture= new();

        public CallDetailRecordControllerTests()
        {
            this.controller = new CallDetailRecordController(service.Object, configuration.Object, notifier.Object);
            this.configuration.Setup(x => x.GetSection("SupportedExtension").Value).Returns(".csv");
            this.configuration.Setup(x => x.GetSection("LineSeparator").Value).Returns("\n");
        }

        [Fact]
        public async void CallDetailRecordController_NullFileTypeReturnsBadRequest()
        {
            // Act
            var result = controller.Upload(null).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("No file uploaded", badRequestResult.Value);
        }

        [Fact]
        public async void CallDetailRecordController_OtherFileTypeReturnsBadRequest()
        {
            // Arrange
            var content = "some file content";
            var bytes = Encoding.UTF8.GetBytes(content);
            var nonCsvFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "example.txt", "example.txt");

            // Act
            var result = controller.Upload(nonCsvFile).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("Incorrect format uploaded", badRequestResult.Value);
        }

        [Fact]
        public async void CallDetailRecordController_CorrectFileAndDataShouldProcess()
        {
            // Arrange
            var content = "441216000000,448000000000,16/08/2016,14:21:33,43,0,C5DA9724701EEBBA95CA2CC5617BA93E4,GBP,2";
            var bytes = Encoding.UTF8.GetBytes(content);
            var nonCsvFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "example.csv", "example.csv");

            // Act
            var result = controller.Upload(nonCsvFile).GetAwaiter().GetResult();

            // Assert
            Assert.Single(service.Invocations);
            Assert.IsType<OkObjectResult>(result);
            var badRequestResult = (OkObjectResult)result;
            Assert.Equal("File uploaded successfully", badRequestResult.Value);
            
        }
    }    
}