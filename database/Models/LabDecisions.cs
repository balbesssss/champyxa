using System;
using api.DB.Models;

namespace api.database.Models;

public class LabDecisions
{
    public Guid Id { get; set; }
    public RawMaterialBatches RawMaterialBatchesId { get; set; }
    public ProductBatches ProductBatchesId { get; set; }
    public string Decision { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public User CreatedBy { get; set; }
}
