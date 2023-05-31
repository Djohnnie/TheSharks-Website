using FluentMigrator;
using System.Data;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220506144639)]
public class Migration_20220506144639 : Migration
{
    private const string Id = "Id";
    private const string AspNetUsers = "AspNetUsers";
    private const string MemberEnrollments = "MemberEnrollments";
    private const string GuestEnrollments = "GuestEnrollments";
    private const string Enrollments = "Enrollments";

    public override void Down()
    {
        Delete.ForeignKey("FK_GuestEnrollments_Enrollments_EnrollmentId").OnTable(MemberEnrollments);
        Delete.ForeignKey("FK_MemberEnrollments_Enrollments_EnrollmentId").OnTable(MemberEnrollments);
        Delete.ForeignKey("FK_MemberEnrollments_AspNetUsers_RegistreeMemberId").OnTable(MemberEnrollments);
        Delete.Index("IX_MemberEnrollments_MemberId").OnTable(MemberEnrollments);
        Delete.Table(MemberEnrollments);
        Delete.Table(GuestEnrollments);
    }

    public override void Up()
    {
        Create.Table(MemberEnrollments)
            .WithColumn(Id).AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .ForeignKey("FK_MemberEnrollments_Enrollments_EnrollmentId", Enrollments, Id)
            .OnDelete(Rule.Cascade)
            .WithColumn("AsDiver").AsBoolean().NotNullable()
            .WithColumn("RegistreeId").AsGuid().NotNullable().Indexed("IX_MemberEnrollments_RegistreeMemberId")
            .ForeignKey("FK_MemberEnrollments_AspNetUsers_RegistreeMemberId", AspNetUsers, Id)
            .OnDelete(Rule.None);

        Create.Table(GuestEnrollments)
            .WithColumn(Id).AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .ForeignKey("FK_GuestEnrollments_Enrollments_EnrollmentId", Enrollments, Id)
            .OnDelete(Rule.Cascade)
            .WithColumn("Registree").AsString(400).NotNullable()
            .WithColumn("GuestType").AsString(30).NotNullable()
            .WithColumn("DiveLevel").AsString(30).NotNullable();
    }
}
