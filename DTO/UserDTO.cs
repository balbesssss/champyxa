namespace api.DTO;

public class UserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Password {get;set;} = string.Empty;
    public Guid RoleId {get;set;} 
    public Guid DepartmentId {get;set;}
    
}
