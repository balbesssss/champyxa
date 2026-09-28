using System;

namespace api.DB.Models;

public class User
{
    public Guid ID {get;set;}
    public string Name { get; set; } = string.Empty;
    public Role Role {get;set;} 
    public Department Department {get;set;}
    public string PasswordHash {get;set;}
    public DateTime CreatedAt {get;set;}
    
}
