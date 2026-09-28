using System;

namespace api.database.Models;

public class LabTestParameters
{
    public Guid Id { get; set; }
    public LabTests LabTestsId { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public decimal ActualValue { get; set; }
    public bool IsRequired { get; set; } = true;
}
