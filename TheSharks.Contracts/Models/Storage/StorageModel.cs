namespace TheSharks.Contracts.Models.Storage;

public class StorageModel
{
    public string? Name { get; set; }
    public string? Uri { get; set; }
    public Stream? Content { get; set; }
    public string? ContentType { get; set; }
}