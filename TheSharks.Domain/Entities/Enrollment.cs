namespace TheSharks.Domain.Entities;

public abstract class Enrollment
{
    public Guid Id { get; set; }
    public string? Remark { get; set; }
    public bool AsDiver { get; set; }
    public Member Registrator { get; set; }
    public Activity Activity { get; set; }
    public Guid? OpenWaterTestId { get; set; }
    public OpenWaterTest? OpenWaterTest { get; set; }
}