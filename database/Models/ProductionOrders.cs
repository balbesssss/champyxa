using api.DB.Models;

namespace api.database.Models;

public class ProductionOrders
{
    public Guid Id { get; set; }
    public Products ProductId { get; set; }
    public Recipes RecipesId { get; set; }
    public TechCard TechCardId { get; set; }
    public decimal Quantity { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public User CreatedBy { get; set; }
}
