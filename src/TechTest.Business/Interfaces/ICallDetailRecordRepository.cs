﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Models;
using TechTest.Business.Models.ResponseModels;

namespace TechTest.Business.Interfaces
{
    public interface ICallDetailRecordRepository
    {
        Task<int> AddCallRecords(List<CallDetailRecord> records);

        Task<CountCallsAndDuration> GetTotalDurationAndNumberOfCallsInTimeRange(CallFilters range);

        Task<IEnumerable<CallDetailRecord>> GetAllCallRecordsForCallerId(CallFilters filters);

        Task<IEnumerable<CallDetailRecord>> RetriveNumberMostExpensiveCalls(CallFilters filters);
    }
}