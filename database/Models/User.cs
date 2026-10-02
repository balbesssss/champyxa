using System;

namespace api.DB.Models;

public class User
{
    public string Name { get; set; } = string.Empty;
    public string PasswordHash {get;set;} = string.Empty;
    public Guid RoleId {get;set;} 
    public Guid DepartmentId {get;set;}
    
}
