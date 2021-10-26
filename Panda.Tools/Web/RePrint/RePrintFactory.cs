using System.Text.RegularExpressions;

namespace Panda.Tools.Web.RePrint;

public class RePrintFactory
{
    public async Task<RePrintPostModel> RePrint(string url)
    {
        var matchRes =  Regex.Match(url, @"(?<=\/\/).*?(?=\/)");
        IRePrint rePrint = null;
        switch (matchRes.Value)
        {
            case "blog.csdn.net":
                Console.WriteLine("csdn");
                rePrint = new CSDNRePrint();
                break;
            case "www.cnblogs.com":
                Console.WriteLine("cnblog");
                rePrint = new CNBlogRePrint();
                break;
        }
        var res =  await rePrint?.RePrint(url)!;
        return res;
    }
}

public class RePrintPostModel
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string Images { get; set; }
}