using System.Diagnostics;
using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Model.Bucket;
using COSXML.CosException;
using COSXML.Transfer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Exception;
using Panda.Tools.Web;

namespace Panda.Tools.FileStorage;

/// <summary>
/// 腾讯云COS存储
/// </summary>
public class CosFileStorage : IFileStorage
{
    private readonly IDicDataProvider _dicDataProvider;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CosFileStorage(IDicDataProvider dicDataProvider, IWebHostEnvironment webHostEnvironment)
    {
        _dicDataProvider = dicDataProvider;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<UploadFileResult> SaveAsync(byte[] bytes, string name)
    {
        _dicDataProvider.SetDefaultGroupName("tencent_cos");
        var config = new CosXmlConfig.Builder()
            .SetRegion(await _dicDataProvider
                .GetDefaultGroupName(
                    "cos_region")) // 设置默认的区域, COS 地域的简称请参照 https://cloud.tencent.com/document/product/436/6224
            .Build();

        var secretId =
            await _dicDataProvider
                .GetDefaultGroupName(
                    "secret_id"); // 云 API 密钥 SecretId, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
        var secretKey =
            await _dicDataProvider
                .GetDefaultGroupName(
                    "secret_key"); // 云 API 密钥 SecretKey, 获取 API 密钥请参照 https://console.cloud.tencent.com/cam/capi
        long durationSecond = 600; //每次请求签名有效时长，单位为秒
        QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
            secretKey, durationSecond);
        CosXml cosXml = new CosXmlServer(config, qCloudCredentialProvider);
        var transferConfig = new TransferConfig();

        // 初始化 TransferManager
        var transferManager = new TransferManager(cosXml, transferConfig);
        // 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
        var bucket = await _dicDataProvider.GetDefaultGroupName("bucket");
        //保存到本地
        var yyyyMMdd = DateTime.Now.ToString("yyyyMMdd");
        var cosPath = $"/{yyyyMMdd}/{name}"; //对象在存储桶中的位置标识符，即称对象键

        
        var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Content", "Upload", yyyyMMdd);
        if (Directory.Exists(localPath) == false)
        {
            Directory.CreateDirectory(localPath);
        }

        var srcPath = Path.Combine(localPath, name); //本地文件绝对路径
        await File.WriteAllBytesAsync(srcPath, bytes);
        // 上传对象
        var uploadTask = new COSXMLUploadTask(bucket, cosPath);
        uploadTask.SetSrcPath(srcPath);

        try
        {
            uploadTask.progressCallback = (completed, total) =>
                Console.WriteLine($"progress = {completed * 100.0 / total:##.##}%");
            var result = await transferManager.UploadAsync(uploadTask);
            Console.WriteLine(result.GetResultInfo());
            File.Delete(srcPath);
        }
        catch (System.Exception e)
        {
            Debug.Fail(e.Message, e.StackTrace);
            throw new UserException("上传失败,请检查配置是否正确");
        }

        return new UploadFileResult()
        {
            Success = true,
            Url = cosPath
        };
    }
}

public static class CosExtensions
{
    public static IServiceCollection AddTencentCos(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileStorage, CosFileStorage>();
        return serviceCollection;
    }
}

public class CosOption
{
    public string SecretId { get; set; }

    public string SecretKey { get; set; }
}