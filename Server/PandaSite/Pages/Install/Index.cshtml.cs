using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PandaSite.Pages.Install;

public class Index : PageModel
{
    public SiteInitModel SiteInitModel { get; set; }

    public void OnGet()
    {
    }
}

public class SiteInitModel
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    [Display(Name = "数据库连接"), Required(ErrorMessage = "这个字段是必须的")]
    public string DbConnectString { get; set; }
}