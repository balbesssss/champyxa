namespace api.DB.Models;
public class Recipes
{
    public Guid Id { get; set; }
    public Products ProductId{ get; set; }
    public int Version { get; set; }     
    public string StatusProduct {get;set;}
    public DateTime CreastedAt { get; set; }
    public User CreatedBy { get; set; }
    public string Comment { get; set; }
    
}