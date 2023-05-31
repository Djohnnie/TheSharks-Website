namespace TheSharks.Domain.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string FileName { get; set; }
    public bool IsImportant { get; set; }
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;
}