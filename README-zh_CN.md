# Panda博客系统

<div align="center">

一个使用`ASP.NET Core MVC 7.0`开发的`博客`系统，目前正在开发中...

 ![dotnet-version](https://img.shields.io/badge/.NET%207.0-blue)  ![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio%20-2022-blueviolet)     [![Github](https://img.shields.io/badge/%20-github-%2324292e)](https://github.com/coolqingcheng/Panda) [![Github stars](https://img.shields.io/github/stars/coolqingcheng/Panda)](https://github.com/coolqingcheng/Panda)

 </div>

[English](./README.md) | 简体中文

## ✨ 1. 特性

1. 使用`ASP.NET Core MVC 7.0`开发
2. 后台前端使用Vue 3.0开发

## 🌈 2. 在线示例

网上冲浪博客：[https://iwscl.com/](https://iwscl.com/)

## 🖥 3. 使用

支持环境

- .NET 7.0
- Visual Studio 2022
- MySQL

### 3.1 前台

主工程目录`"/Panda"`，使用 `ASP.NET Core MVC 7.0` 开发，包括网站前台，正确运行前，请先对项目进行配置，请看下面说明。

1. 配置数据库连接字符串

在工程`Panda`的`appsettings.json`文件中添加节点，配置MySQL连接字符串，建议在`appsettings.Development.json`及`appsettings.Production.json`中配置：

```json
"ConnectionStrings": {
  "MYSQL_DB": "server=localhost;user=[username];database=[databasename];port=[port];password=[password];SslMode=None"
}
```

2. 数据迁移

打开程序包控制台，选择项目：`Panda.Entity`，执行以下命令：

```shell
Update-Database
```

如果删除了`Panda.Entity/Migrations`目录，就执行以下命令即可

```shell
Add-Migration InitDB
Update-Database
```

3. 生成数据种子

以上2个步骤完成后，运行项目，访问链接`http://localhost:5151/initaccount`执行种子数据生成，此方法写在工程`Panda.Admin`的`AccountController`中

```C#
[AllowAnonymous]
[HttpGet("/initaccount")]
public async Task<IActionResult> Test()
{
    await _accountService.InitAccount();
    return Content("初始化账号成功");
}
```

默认账号如下，后台管理使用：

```shell
account: admin
password: admin
```

4. 发布

```shell
dotnet publish
```

### 3.2 后台

后台后端由前台`"/Panda"`项目提供，本小节只对`Vue 3.0`开发的后台前端进行说明。

- 安装包

```shell
npm install
```

- 调试运行

```shell
npm run serve
```

- 发布

```shell
npm run build
```

## 💕 4. 支持本项目

## ☀️ 5. License

MIT

本项目得到  [jetbrains](https://jb.gg/OpenSourceSupport) 的支持

![jetbrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg)
