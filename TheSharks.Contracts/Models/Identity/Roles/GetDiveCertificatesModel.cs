namespace TheSharks.Contracts.Models.Identity.Roles;

public class GetDiveCertificatesModel
{
    public List<DiveCertificateModel> DiveCertificates { get; set; }
}

public class DiveCertificateModel
{
    public string Name { get; set; }
}