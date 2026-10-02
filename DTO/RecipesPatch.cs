namespace api.DTO;

public class RecipesPatch
{
    public Guid? ProductId{ get; set; }
    public int? Version { get; set; }     
    public string? StatusProduct {get;set;}
    public Guid? CreatedBy { get; set; }
    public string? Comment { get; set; }
}
