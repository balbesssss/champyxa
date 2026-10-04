using System;

namespace api.DTO;

public class UserPatch
{
    public string? Name { get; set; } = null;
    public string? PasswordHash {get;set;} = null;
    public Guid? RoleId {get;set;} = null;
    public Guid? DepartmentId {get;set;} = null;
}
