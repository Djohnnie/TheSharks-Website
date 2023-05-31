namespace TheSharks.Domain.Entities;

public class Picture
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string StorageUrl { get; set; }
    public Gallery Gallery { get; set; }
}