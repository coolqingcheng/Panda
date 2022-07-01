namespace Panda.Tools.Auth.Permission;


public class PGroupAttribute : Attribute
{
    public PGroupAttribute(string groupName)
    {
        GroupName = groupName;
    }
    public string GroupName { get; set; }
}