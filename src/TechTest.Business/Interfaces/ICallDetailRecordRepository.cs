using TechTest.Business.Models;
using TechTest.Business.Models.ResponseModels;

namespace TechTest.Business.Interfaces
{
    public interface ICallDetailRecordRepository
    {
        Task<int> AddCallRecords(List<CallDetailRecord> records);

        Task<CountCallsAndDuration> GetTotalDurationAndNumberOfCallsInTimeRange(InputModel range);

        Task<List<CallDetailRecord>> GetAllCallRecordsForCallerId(CallFilters filters);

        Task<List<CallDetailRecord>> RetriveNumberMostExpensiveCalls(CallFilters filters);

        Task<CallDetailRecord> RetriveCallById(string id);
    }
}
