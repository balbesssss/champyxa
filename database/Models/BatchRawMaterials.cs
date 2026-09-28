using System;

namespace api.database.Models;

public class BatchRawMaterials
{
    public Guid Id { get; set; }
    public ProductBatches ProductBatchesId { get; set; }
    public RawMaterialBatches RawMaterialBatchesId { get; set; }
    public decimal QuantityUsed { get; set; }
}
