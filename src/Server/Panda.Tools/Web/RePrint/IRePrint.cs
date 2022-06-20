namespace Panda.Tools.Web.RePrint;

public interface IRePrint
{
    Task<RePrintPostModel> RePrint(string url);
}