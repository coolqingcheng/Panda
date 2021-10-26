namespace Panda.Tools.Web.RePrint;

public class RePrintFactory
{
    public async Task<RePrintPostModel> RePrint(string url)
    {
        var model = new RePrintPostModel();
        IRePrint rePrint = new CNBlogRePrint();
        var res =  await rePrint.RePrint(url);
        return res;
    }
}

public class RePrintPostModel
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string Images { get; set; }
}