using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Models.Enums;

namespace TechTest.Business.Models
{
    public class CallFilters
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? CallerID { get; set; }

        public TypeOfCall? CallType { get; set; }

        public int? NumberOfMostExpensiveCallsToRetrieve { get; set; }
    }
}
