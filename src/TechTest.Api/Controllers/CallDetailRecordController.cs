using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechTest.Business.Interfaces;
using TechTest.Business.Models;
using TechTest.Business.Models.ResponseModels;

namespace TechTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallDetailRecordController : ControllerBase
    {
        private readonly ICallDetailRecordService service;
        private readonly IConfiguration configuration;

        public CallDetailRecordController(ICallDetailRecordService service, IConfiguration configuration)
        {
            this.service = service;
            this.configuration = configuration;
        }

        [HttpPost("GetTotalDurationOfCallsInTimeRange")]
        public async Task<CountCallsAndDuration> GetTotalDurationOfCallsInTimeRange(CallFilters filter) 
        {
            var value = await this.service.GetTotalDurationOfCallsInTimeRange(filter);
            return value;
        }

        [HttpPost("uploadCsv")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            if (!file.FileName.EndsWith(configuration.GetSection("SupportedExtension").Value))
            {
                return BadRequest("Incorrect format uploaded");
            }

            string fileContent;
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await streamReader.ReadToEndAsync();
                await this.service.AddCallRecords(fileContent.Split(configuration.GetSection("LineSeparator").Value).ToList());
            }

            return Ok("File uploaded successfully");
        }
    }
}
