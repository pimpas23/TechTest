using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Interfaces;
using TechTest.Business.Models;
using TechTest.Business.Models.ResponseModels;
using TechTest.Data.Context;

namespace TechTest.Data.Repository
{
    public class CallDetailRecordRepository : ICallDetailRecordRepository
    {
        private readonly MyDbContext Db;
        private readonly DbSet<CallDetailRecord> DbSet;

        public CallDetailRecordRepository(MyDbContext db)
        {
            Db = db;
            DbSet = db.Set<CallDetailRecord>();
        }

        public async Task<int> AddCallRecords(List<CallDetailRecord> records)
        {
            await DbSet.AddRangeAsync(records);
            return await SaveChanges();
        }

        public async Task<IEnumerable<CallDetailRecord>> GetAllCallRecordsForCallerId(CallFilters filters)
        {
            var records = await DbSet.ToListAsync();
            if(filters.EndDate!=default && filters.StartDate != default)
            {
                records = records.Where(cr => cr.CallDateEndTime> filters.StartDate && cr.CallDateEndTime < filters.EndDate).ToList();
            }

            if(filters.CallType.HasValue) 
            {
               records = records.Where(cr => cr.TypeOfCall==filters.CallType).ToList();
            }
            
            return records.Where(c => c.CallerNumber==filters.CallerID );
        }

        public async Task<CountCallsAndDuration> GetTotalDurationAndNumberOfCallsInTimeRange(CallFilters range)
        {
            var records = await DbSet.ToListAsync();

            records=records.Where(cr => (cr.CallDateEndTime > range.StartDate &&
            cr.CallDateEndTime < range.EndDate) &&
            cr.TypeOfCall == range.CallType).ToList();

            return new CountCallsAndDuration 
            { 
                CountCalls = records.Count(),
                DurationCalls = records.Sum(cr => cr.CallDuration) 
            };
        }

        public async Task<IEnumerable<CallDetailRecord>> RetriveNumberMostExpensiveCalls(CallFilters filters)
        {
            var records = await DbSet.ToListAsync();
            records = records.OrderByDescending(cr => cr.Cost).ToList().Take(filters.NumberOfMostExpensiveCallsToRetrieve ?? 1).ToList();
            return records;
        }

        public async Task<int> SaveChanges()
        {
            var changes = await Db.SaveChangesAsync();
            return changes;
        }
    }
}
