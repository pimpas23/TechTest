using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using TechTest.Api.IntegrationTests.Config;
using TechTest.Business.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace TechTest.Api.IntegrationTests
{
   
    [Collection("IntegrationTests")]
    public class CallDetailRecordControllerTests : IntegrationTestBase
    {
        ////[Fact]
        ////public async Task CallDetailRecordControllerTests_AddCsvWithData_AddsDataToDatabase()
        ////{
        ////    // Arrange
        ////    var client = new AppFactory<Program>().CreateClient();
        ////    var csvContent = "441216000000,448000000000,16/08/2016,14:21:33,43,0,C5DA9724701EEBBA95CA2CC5617BA93E4,GBP,2";
        ////    var csvBytes = Encoding.UTF8.GetBytes(csvContent);
        ////    var csvFileContent = new ByteArrayContent(csvBytes);

        ////    using var formData = new MultipartFormDataContent
        ////    {
        ////        { csvFileContent, "csvFile", "data.csv" }
        ////    };
        ////    var request = new RestRequest { Method = Method.Post };
        ////    request.AddFile("csvFile", csvBytes, "data.csv");

        ////    // Act
        ////    var response = await client.PostAsync("/api/CallDetailRecord/UploadCsv", formData);

        ////    // Assert
        ////    response.EnsureSuccessStatusCode();
        ////}
        
        [Fact]
        public async Task CallDetailRecordControllerTests_AddCsvWithData_AddsDataToDatabase()
        {
            // Arrange
            var client = new AppFactory<Program>().CreateClient();
            var currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string integrationTestsFolder = Path.Combine(currentLocation, "..", "..", "..");
            var filePath = Path.GetFullPath(integrationTestsFolder); // Replace this with the path to your test file
            var fileContent = File.ReadAllBytes(Path.Combine(filePath, "techtest_cdr_dataset.csv"));
            var fileName = Path.GetFileName(filePath);

            using var content = new MultipartFormDataContent();
            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            // Add the file content to the request
            content.Add(new ByteArrayContent(fileContent), "file.csv", fileName);

            var response = await client.PostAsync("/api/CallDetailRecord/UploadCsv", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CallDetailRecordControllerTests_GetByID_returnsCorrect()
        {
            // Arrange
            var client = new AppFactory<Program>().CreateClient();
            var id = "C5DA9724701EEBBA95CA2CC5617BA93E4";
            // Act
            var response = await client.GetAsync($"/api/CallDetailRecord/GetByID?id={id}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CallDetailRecord>(responseBody);

            // Assert
            response.EnsureSuccessStatusCode();
            
            Assert.NotNull(result);
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task CallDetailRecordControllerTests_GetByID_returnsNotFound()
        {
            // Arrange
            var client = new AppFactory<Program>().CreateClient();
            var id = "C5DA9724701EEBBA95CA2CC5617BA93E5";
            // Act
            var response = await client.GetAsync($"/api/CallDetailRecord/GetByID?id={id}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CallDetailRecord>(responseBody);

            // Assert
            Assert.NotNull(result);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


    }
}
