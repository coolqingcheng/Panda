﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using Panda.Services.Sys;

namespace PandaSite.Pages.Install;

public class Index : PageModel
{
    public SiteInitModel SiteInitModel { get; set; }

    private AccountService _accountService;


    public Index(AccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<IActionResult> OnGet()
    {
        var isInit = await _accountService.CheckSysInitStatus();
        if (isInit)
        {
            return Redirect("/");
        }

        return Page();
    }

    public async Task InstallSave()
    {
    }

    public bool IsConnect = true;


    public async Task<IActionResult> TestConnect()
    {
        try
        {
            await using var conn = new MySqlConnection(SiteInitModel.DbConnectString);
            await conn.OpenAsync();
        }
        catch (Exception e)
        {
            IsConnect = false;
        }

        return Page();
    }
}

public class SiteInitModel
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    [Display(Name = "数据库连接字符串"), Required(ErrorMessage = "这个字段是必须的")]
    public string DbConnectString { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Display(Name = "用户名"), Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; }

    /// <summary>
    /// 用户密码
    /// </summary>
    [Display(Name = "密码"), Required(ErrorMessage = "密码不能为空")]
    public string UserPwd { get; set; }
}