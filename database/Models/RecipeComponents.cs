namespace api.DB.Models;

public class RecipeComponents
{
    public Guid Id { get; set; }
    public Recipes ResipesId { get; set; }
    public RawMaterials RawMaterialId { get; set; }
    public decimal Share { get; set; }
    public int LoadOrder { get; set; }
}