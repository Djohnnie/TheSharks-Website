namespace TheSharks.Contracts.Models.Activities.BaseModels;

public class GetActivityBaseModel : ActivityBaseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ActivityType { get; set; }
    public Guid ResponsibleId { get; set; }
    public string ResponsibleFirstName { get; set; }
    public string ResponsibleLastName { get; set; }
    public IEnumerable<ActivityEnrollmentModel> Enrollments { get; set; }
}

public class ActivityEnrollmentModel
{
    public Guid? Id { get; set; }
    public string RegistratorFirstName { get; set; }
    public string RegistratorLastName { get; set; }
    public string Registree { get; set; }
    public Guid? RegistreeId { get; set; }
    public string RegistreePhoneNumber { get; set; }
    public string RegistreeEmail { get; set; }
    public bool AsDiver { get; set; }
    public string? DiveCertificate { get; set; }
    public string? Remark { get; set; }
    public string? OpenWaterTestTitle { get; set; }
    public string? OpenWaterTestContent { get; set; }
}