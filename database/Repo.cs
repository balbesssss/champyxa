using Dapper;

public class ProductRepository
{
    private readonly string _connectionString;
    
    public ProductRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    } 
    
}
