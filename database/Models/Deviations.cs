using System;
using api.db.Models;

namespace api.database.Models;

public class Deviations
{
    public Guid Id { get; set; }
    public BatchSteps BatchStepsId { get; set; }
    public StepParameters StepParametersId { get; set; }
    public decimal PlannedValue { get; set; }
    public decimal ActualValue {get;set;}
    public string Severity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Comment { get; set; }
}
