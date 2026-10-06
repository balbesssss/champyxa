namespace api.DB.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password {get;set;} = string.Empty;
    public Role RoleId {get;set;} 
    public Guid DepartmentId {get;set;}
    public DateTime CreatedAt { get; set; }
}
