namespace TheSharks.Contracts.Models.Enrollments;

public class EnrollmentBaseModel
{
    public Guid RegistratorId { get; set; }
    public Guid ActivityId { get; set; }
    public bool AsDiver { get; set; }
    public string? Remark { get; set; }
    public Guid? OpenWaterTestId { get; set; }
}

public class EnrollmentMemberModel : EnrollmentBaseModel
{
    public Guid RegistreeId { get; set; }
    public string Registree { get; set; }
}

public class EnrollmentGuestModel : EnrollmentBaseModel
{
    public string Registree { get; set; }
    public string DiveCertificate { get; set; }
}