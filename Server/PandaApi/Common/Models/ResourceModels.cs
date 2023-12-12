namespace PandaApi.Common.Models;

public class ResourceModels
{
    public string Url { get; set; }
    public string Suffix { get; set; }
    public string Domain { get; set; }

    public double Size { get; set; }

    public string SizeName => $"{Size / 1000}kb";

    public DateTime CreateTime { get; set; }

    public StorageType StorageType { get; set; }

    public string? StorageName => StorageType.GetDescription();
}