﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Business.Models.Enums;

namespace TechTest.Business.Models;

public class CallDetailRecord
{
    [StringLength(15)]
    public string? CallerNumber { get; set; }
    [StringLength(15)]
    public string? RecipientNumber { get; set; }

    public DateTime CallDateEndTime { get; set; }

    public int CallDuration { get; set; }

    public Guid Id { get; set; }

    public double Cost { get; set; }

    public TypeOfCall TypeOfCall { get; set; }

    public Currency Currency { get; set; }
}
