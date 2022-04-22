# Panda blog system

<div align="center">

One use `ASP.NET Core MVC 7.0` developed `blog` system, which is currently under development

 ![dotnet-version](https://img.shields.io/badge/.NET%207.0-blue)  ![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio%20-2022-blueviolet)     [![Github](https://img.shields.io/badge/%20-github-%2324292e)](https://github.com/coolqingcheng/Panda) [![Github stars](https://img.shields.io/github/stars/coolqingcheng/Panda)](https://github.com/coolqingcheng/Panda)
</div>

English | [简体中文](README-zh_CN.md)

## ✨ 1. Characteristics

1. Use `ASP MVC. Net 6.0` development
2. The background front end is developed using `Vue 3.0`

## 🌈 2. Online example



网上冲浪博客：[https://iwscl.com/](https://iwscl.com/)

## 🖥 3. Use

Support environment

- .NET 7.0
- Visual Studio 2022
- MySQL

### 3.1 front desk

Main project directory `"/panda"`, using `ASP.NET Core MVC 7.0` development, including the website foreground. Please configure the project before running correctly. Please see the following instructions.

1. Configure database connection string

In the project `Panda`, add nodes to the `appsettings.json` file and configure MySQL connection strings. It is recommended to use `appsettings.Development.json` and `appsettings.Production.json` configuration:

```json
"ConnectionStrings": {
  "MYSQL_DB": "server=localhost;user=[username];database=[databasename];port=[port];password=[password];SslMode=None"
}
```

2. Data migration

Open the package console and select the project: `panda Entity`, execute the following command:

```shell
Update-Database
```

If you delete `Panda.Entity/Migrations` directory, just execute the following command

```shell
Add-Migration InitDB
Update-Database
```

3. Release

```shell
dotnet publish
```

### 3.2 background

The background back-end is provided by the foreground `"/Panda"` project. This section only describes the background front-end developed by `Vue 3.0`.

- Installation package

```shell
npm install
```

- Commissioning operation

```shell
npm run serve
```

- Release

```shell
npm run build
```

## 💕 4. Support the project


## ☀️ 5. License

MIT

The project is support of [JetBrains](https://jb.gg/OpenSourceSupport)

![jetbrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg)
