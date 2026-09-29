using System.ComponentModel.DataAnnotations.Schema;
using api.DB.Models;
using Dapper;
using Npgsql;

namespace api.db.Repo;
public class ProductRepository
{
    private readonly string _connectionString;
    
    public ProductRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default")!;
    } 

    private async Task<IEnumerable<T>> QueryAsync<T>(string sql,object? param = null)
    {
        using var con = new NpgsqlConnection(_connectionString);
        return await con.QueryAsync<T>(sql,param);
    }
    private async Task<T?> QueryFirstAsync<T>(string sql, object? param = null)
    {
        using var con = new NpgsqlConnection(_connectionString);
        return await con.QueryFirstOrDefaultAsync<T>(sql,param);
    }
    private async Task<int> ExecuteAsync (string sql, object? param = null)
    {
        using var con = new NpgsqlConnection(_connectionString);
        return await con.ExecuteAsync(sql,param);
    }
    

    public Task<IEnumerable<Products>> GetProductsAsync()
        => QueryAsync<Products>("SELECT * FROM Products"); 

    public async Task<int> CreateProduct(Products p)
    {
        p.Id = Guid.NewGuid();
        p.CreatedAt = DateTime.UtcNow;
        return await ExecuteAsync(@"insert into Products (Id, Code,Name, Type, Form, Status,CreatedAt) 
        Values (@Id, @Code, @Name,@Type,@Form,@Status,@CreatedAt)", p);
    }

    
}
