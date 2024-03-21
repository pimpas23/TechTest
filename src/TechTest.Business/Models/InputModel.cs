using TechTest.Business.Models.Enums;

namespace TechTest.Business.Models;

public class InputModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public TypeOfCall CallType { get; set; }
}
