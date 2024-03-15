using Microsoft.AspNetCore.Mvc;
using TechTest.Business.Interfaces;
using TechTest.Business.Models;
using TechTest.Business.Models.ResponseModels;

namespace TechTest.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CallDetailRecordController : ControllerBase
{
    private readonly ICallDetailRecordService service;
    private readonly IConfiguration configuration;

    public CallDetailRecordController(
        ICallDetailRecordService service,
        IConfiguration configuration)
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

    [HttpGet("GetByID")]
    public async Task<ActionResult<CallDetailRecord?>> GetTotalDurationOfCallsInTimeRange(string id)
    {
        var value = await this.service.RetriveCallById(id);

        if (value != null)
        {
            return value;
        }

        return NotFound();
    }

    [HttpPost("GetAllCallsByCallerID")]
    public async Task<ActionResult<List<CallDetailRecord?>>> GetAllCallsByCallerID(CallFilters filter)
    {
        var values = await this.service.GetAllCallRecordsForCallerId(filter);

        if (values != null)
        {
            return values;
        }

        return NotFound();
    }

    [HttpPost("UploadCsv")]
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
