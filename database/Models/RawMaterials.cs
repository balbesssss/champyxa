namespace api.DB.Models;

public class RawMaterials
{
    public Guid Id {get;set;}
    required public string Code {get;set;}
    public string ProductName {get;set;} = string.Empty;
    public string Unit {get;set;}
}