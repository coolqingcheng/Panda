namespace Panda.Tools.Attributes;

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

[AttributeUsage(AttributeTargets.Class)]
public class PermissionGroupAttribute : Attribute
{
    public PermissionGroupAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

/// <summary>
/// 权限控制不拦截
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class NoPermissionAttribute : Attribute
{

}