using System;
using api.db.Models;

namespace api.database.Models;

public class BatchStepValues
{
    public Guid Id { get; set; }
    public BatchSteps BatchStepId { get; set; }
    public StepParameters StepParameterId { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; }
}
