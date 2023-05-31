namespace TheSharks.Domain.Entities;

public class Component
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public int Position { get; set; }
    public Page Page { get; set; }
}