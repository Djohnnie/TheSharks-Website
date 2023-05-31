namespace TheSharks.Storage.Options;

public class StorageOptions
{
    public const string Storages = "Storages";

    public StorageItemOptions Documents { get; set; }
    public StorageItemOptions Pictures { get; set; }
}

public class StorageItemOptions
{
    public string StorageConnectionString { get; set; }
    public string ContainerName { get; set; }
}