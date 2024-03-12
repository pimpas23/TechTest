using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Models;
using TechTest.Business.Models.ResponseModels;

namespace TechTest.Business.Interfaces
{
    public interface ICallDetailRecordService
    {
        Task AddCallRecords(List<string> records);

        Task<CountCallsAndDuration> GetTotalDurationOfCallsInTimeRange(CallFilters range);

        Task GetAllCallRecordsForCallerId(CallFilters filters);

        Task RetriveNumberMostExpensiveCalls(CallFilters filters);
    }
}
