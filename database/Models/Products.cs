namespace api.DB.Models; 

public class Products
{
    public Guid Id {get;set;}
    public string Code {get;set;}
    public string Name {get;set;} = string.Empty;
    public string Type {get;set;}
    public string Form {get;set;} = string.Empty;
    public string Status {get;set;}
    public DateTime CreatedAt {get;set;}
}

