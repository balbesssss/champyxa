using api.DB.Models;

namespace api.database.Models;

public class ProductBatches
{
    public Guid Id { get; set; }
    public ProductionOrders ProductionOrderId { get; set; }
    public string BatchNumber { get; set; }
    public string Status { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public Equipment EquipmentId { get; set; }
}
