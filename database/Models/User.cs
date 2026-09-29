using System;

namespace api.DB.Models;

public class User
{
    public Guid ID {get;set;}
    public string Name { get; set; } = string.Empty;
    public string PasswordHash {get;set;}
    public Guid Role {get;set;} 
    public Guid Department {get;set;}
    public DateTime CreatedAt {get;set;}
    
}
