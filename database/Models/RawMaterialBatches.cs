using System;
using api.DB.Models;

namespace api.database.Models;

public class RawMaterialBatches
{
    public Guid Id { get; set; }
    public RawMaterials RawMaterialsId { get; set; }
    public string SupplinerbatchNumber { get; set; }
    public string Suppliner { get; set; }
    public DateTime ReceivedAt { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public string LabStatus { get; set; }
}
