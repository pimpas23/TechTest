using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Interfaces;
using TechTest.Business.Models;
using TechTest.Business.Models.Enums;
using TechTest.Business.Models.ResponseModels;

namespace TechTest.Business.Services;

public class CallDetailRecordService : ICallDetailRecordService
{
    private readonly ICallDetailRecordRepository callRepository;

    public CallDetailRecordService(ICallDetailRecordRepository callRepository)
    {
            this.callRepository = callRepository;
    }
    public async Task AddCallRecords(List<string> records)
    {
        var data = this.ParseData(records);
        await callRepository.AddCallRecords(data);
    }

    public Task GetAllCallRecordsForCallerId(CallFilters filters)
    {
        throw new NotImplementedException();
    }

    public Task<CountCallsAndDuration> GetTotalDurationOfCallsInTimeRange(CallFilters range)
    {
        return this.callRepository.GetTotalDurationAndNumberOfCallsInTimeRange(range);
    }

    public Task RetriveNumberMostExpensiveCalls(CallFilters filters)
    {
        return this.RetriveNumberMostExpensiveCalls(filters);
    }

    private List<CallDetailRecord> ParseData(List<string> records)
    {
        var list = new List<CallDetailRecord>();

        if (!records.FirstOrDefault().Contains(":"))
        {
            records.RemoveAt(0);
        }

        foreach (var record in records)
        {
            try
            {
                var parseRecord = record.Split(',').ToList();
                var year = Convert.ToInt32(parseRecord[2].Split('/')[2]);
                var month = Convert.ToInt32(parseRecord[2].Split('/')[1]);
                var day = Convert.ToInt32(parseRecord[2].Split('/')[0]);
                var hour = Convert.ToInt32(parseRecord[3].Split(':')[0]);
                var minute = Convert.ToInt32(parseRecord[3].Split(':')[1]);
                var second = Convert.ToInt32(parseRecord[3].Split(':')[2]);
                var emptyGuid = Guid.Empty;
                list.Add(new CallDetailRecord
                {
                    CallerNumber = parseRecord[0],
                    RecipientNumber = parseRecord[1],
                    CallDateEndTime = new DateTime(year, month, day, hour, minute, second),
                    CallDuration = Convert.ToInt32(parseRecord[4]),
                    Cost = Convert.ToDouble(parseRecord[5]),
                    Id = parseRecord[6],
                    Currency = (Currency)Enum.Parse(typeof(Currency), parseRecord[7]),
                    TypeOfCall = (TypeOfCall)Enum.Parse(typeof(TypeOfCall), parseRecord[8]),
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(record.ToString());
                // TODO - send an alert to controller that there was a error in this line
            }
           
        }
        return list;
    }
}
