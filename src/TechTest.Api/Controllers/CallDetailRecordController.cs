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

        public CallDetailRecordController(ICallDetailRecordService service)
        {
            this.service = service;
        }

        [HttpPost("GetTotalDurationOfCallsInTimeRange")]
        public async Task<CountCallsAndDuration> GetTotalDurationOfCallsInTimeRange(CallFilters filter) 
        {
            var value = await this.service.GetTotalDurationOfCallsInTimeRange(filter);
            return value;
        }

        [HttpPost("uploadCsv")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (!file.FileName.EndsWith(".csv"))
            {
                return BadRequest("Incorrect format uploaded");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            try
            {
                string fileContent;
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await streamReader.ReadToEndAsync();
                    await this.service.AddCallRecords(fileContent.Split("\n").ToList());
                }

                return Ok("File uploaded successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
