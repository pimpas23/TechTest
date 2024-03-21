using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Business.Models.Enums;

/// <summary>
/// Type of call enum for the different types of calls that can be made 1-Domestic, 2-International
/// </summary>
public enum TypeOfCall
{
    /// <summary>
    /// Represents a domestic call.
    /// </summary>
    Domestic = 1,
    /// <summary>
    /// Represents an international call.
    /// </summary>
    International = 2,
}
