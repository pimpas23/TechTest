using Microsoft.AspNetCore.Mvc;
using TechTest.Business.Interfaces;
using TechTest.Business.Models;
using TechTest.Business.Models.Enums;
using TechTest.Business.Models.ResponseModels;
using TechTest.Business.Services;

namespace TechTest.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CallDetailRecordController : ControllerBase
{
    private readonly ICallDetailRecordService service;
    private readonly IConfiguration configuration;
    private readonly INotifier notifier;

    public CallDetailRecordController(
        ICallDetailRecordService service,
        IConfiguration configuration,
        INotifier notifier)
    {
        this.service = service;
        this.configuration = configuration;
        this.notifier = notifier;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet("GetTotalDurationOfCallsInTimeRange")]
    public async Task<ActionResult<CountCallsAndDuration>> GetTotalDurationOfCallsInTimeRange([FromQuery] InputModel filter)
    {
        if (!this.AreDatesValid(filter.StartDate, filter.EndDate))
        {
            return BadRequest("Invalid date range, please dont use a gap between dates than more than 30 days!");
        }

        var value = await this.service.GetTotalDurationOfCallsInTimeRange(filter);
        return value;
    }

    [HttpGet("RetrieveMostExpensiveCalls")]
    public async Task<ActionResult<List<CallDetailRecord>>> RetrieveMostExpensiveCalls([FromQuery] CallFilters filter)
    {
        if (!this.AreDatesValid(filter.StartDate, filter.EndDate))
        {
            return BadRequest("Invalid date range, please dont use a gap between dates than more than 30 days!");
        }

        var value = await this.service.RetriveNumberMostExpensiveCalls(filter);
        return value;
    }

    [HttpGet("GetByID")]
    public async Task<ActionResult<CallDetailRecord?>> GetRecordById(string id)
    {
        var value = await this.service.RetriveCallById(id);

        if (value != null)
        {
            return value;
        }

        return NotFound();
    }

    /// <summary>
    /// Gets all calls for given Id, for the call type use: 1 - Domestic, 2 - International
    /// </summary>
    /// <param name="filter"> </param>
    /// <returns></returns>
    [ProducesResponseType(typeof(List<CallDetailRecord>), 200)]
    [HttpGet("GetAllCallsByCallerID")]
    public async Task<ActionResult<List<CallDetailRecord?>>> GetAllCallsByCallerID([FromQuery] CallFilters filter)
    {
        if(!this.AreDatesValid(filter.StartDate, filter.EndDate))
        {
            return BadRequest("Invalid date range, please dont use a gap between dates than more than 30 days!");
        }

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
            try
            {
                await this.service.AddCallRecords(fileContent.Split(configuration.GetSection("LineSeparator").Value).ToList());
            }
            catch (Exception e)
            {
                var str = "";
                foreach (var notification in this.notifier.GetNotifications())
                {
                    str += notification.Message;
                }
                return BadRequest(str);
            }
            
        }

        if(this.notifier.HasNotification())
        {
            var str = "";
            foreach (var notification in this.notifier.GetNotifications())
            {
                str += notification.Message;
            }
            return BadRequest(str);
        }

        return Ok("File uploaded successfully");
    }

    private bool AreDatesValid(DateTime? startDate, DateTime? endDate)
    {
        return startDate < endDate && endDate?.AddDays(-30) <= startDate;
    }
}
