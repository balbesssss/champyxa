using System;
using api.DB.Models;

namespace api.database.Models;

public class LabTests
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string ObjectType { get; set; }
    public RawMaterialBatches RawMaterialBatchesId { get; set; }  
    public ProductBatches ProductBatchesId { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public User CreatedBy { get; set; }
    public string Comment { get; set; }
}
