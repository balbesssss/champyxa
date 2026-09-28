namespace api.DB.Models; 

public class Products
{
    public Guid Id {get;set;}
    required public string Code {get;set;}
    public string ProductName {get;set;} = string.Empty;
    required public string TypeProducts {get;set;}
    public string ProductsForm {get;set;} = string.Empty;
    required public string StatusProduct {get;set;}
    public DateTime DateTime {get;set;}


}

