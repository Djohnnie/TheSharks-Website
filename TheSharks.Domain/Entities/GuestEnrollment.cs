namespace TheSharks.Domain.Entities;

public class GuestEnrollment : Enrollment
{
    public string Registree { get; set; }
    public string DiveLevel { get; set; }
}