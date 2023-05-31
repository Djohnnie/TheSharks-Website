namespace TheSharks.Domain.Entities;

public class Statistics
{
    public Guid Id { get; set; }
    public long SysId { get; set; }
    public string Page { get; set; }
    public DateTime Date { get; set; }
    public bool IsLoggedIn { get; set; }
    public bool IsApp { get; set; }
    public Guid SessionId { get; set; }
}