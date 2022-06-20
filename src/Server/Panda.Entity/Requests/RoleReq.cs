namespace Panda.Entity.Requests;

public class RoleReq
{
}

public class AddRoleReq
{
    public Guid? Id { get; set; }

    public string RoleName { get; set; }
}