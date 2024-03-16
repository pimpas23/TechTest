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

    public Task<List<CallDetailRecord>> GetAllCallRecordsForCallerId(CallFilters filters)
    {
        return this.callRepository.GetAllCallRecordsForCallerId(filters);
    }

    public Task<CountCallsAndDuration> GetTotalDurationOfCallsInTimeRange(InputModel range)
    {
        return this.callRepository.GetTotalDurationAndNumberOfCallsInTimeRange(range);
    }

    public Task<CallDetailRecord> RetriveCallById(string id)
    {
        return this.callRepository.RetriveCallById(id);
    }

    public Task RetriveNumberMostExpensiveCalls(CallFilters filters)
    {
        return this.RetriveNumberMostExpensiveCalls(filters);
    }

    private List<CallDetailRecord> ParseData(List<string> records)
    {
        var list = new List<CallDetailRecord>();
        var doubleDot = ':';
        if (!records.FirstOrDefault().Contains(doubleDot))
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
                var hour = Convert.ToInt32(parseRecord[3].Split(doubleDot)[0]);
                var minute = Convert.ToInt32(parseRecord[3].Split(doubleDot)[1]);
                var second = Convert.ToInt32(parseRecord[3].Split(doubleDot)[2]);
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
                Console.WriteLine(record.ToString() + $"line: {list.Count + 1}");
            }

        }
        return list;
    }
}
