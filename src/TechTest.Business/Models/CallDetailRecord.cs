using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Models.Enums;

namespace TechTest.Business.Models;

/// <summary>
/// CallDetailRecord model for the call detail record
/// </summary>
public class CallDetailRecord
{
    [StringLength(15)]
    public string? CallerNumber { get; set; }
    [StringLength(15)]
    public string? RecipientNumber { get; set; }

    public DateTime CallDateEndTime { get; set; }

    public int CallDuration { get; set; }

    [Key]
    [StringLength(40)]
    public string Id { get; set; }

    public double Cost { get; set; }
    /// <summary>
    /// Type of call enum for the different types of calls that can be made 1-Domestic, 2-International
    /// </summary>
    public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
    /// 0-GBP, 1-USD, 2-EUR, 3-JPY
    /// </summary>
    public Currency Currency { get; set; }
}
