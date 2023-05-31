namespace TheSharks.Domain.Entities;

public class Gallery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int AmountPictures { get; set; }
    public string? UrlFirstPicture { get; set; }
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
    public IList<Picture> Pictures { get; set; }
}