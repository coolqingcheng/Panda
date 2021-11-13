namespace Panda.Tools.Web;

public interface IDicDataProvider
{
    void SetDefaultGroupName(string groupName);
    Task<string?> GetDefaultGroupName(string key);
    Task<string?> Get(string group, string key);
}