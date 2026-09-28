using System;
using api.DB.Models;

namespace api.database.Models;

public class BatchSteps
{
    public Guid ID { get; set; }
    public ProductBatches ProductBatchId { get; set; }
    public TechStep TechStepId { get; set; }
    public string Status { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public User StartedBy { get; set; }
    public string Comment { get; set; }
}
