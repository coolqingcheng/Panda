namespace Panda.Admin.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class PermissionAttribute : Attribute
{
    public PermissionAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 权限名字
    /// </summary>
    public string Name { get; set; }
}