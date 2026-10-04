namespace api.DB.Models;
public class Recipes
{
    public Products ProductId{ get; set; }
    public int Version { get; set; }     
    public string StatusProduct {get;set;}
    public User CreatedBy { get; set; }
    public string Comment { get; set; }
    
}