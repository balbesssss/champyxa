namespace api.DB.Models; 

public class Products
{
    public Guid Id {get;set;}
    required public string Code {get;set;}
    public string ProductName {get;set;} = string.Empty;
    required public string ProductType {get;set;}
    public string ProductForm {get;set;} = string.Empty;
    required public string ProductStatus {get;set;}
    public DateTime DateTime {get;set;}


}

