namespace TheSharks.Domain.Entities;

public class MemberEnrollment : Enrollment
{
    public Member Registree { get; set; }
}