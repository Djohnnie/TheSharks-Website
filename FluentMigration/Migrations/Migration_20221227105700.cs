using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20221227105700)]
public class Migration_20221227105700 : Migration
{
    private const string Enrollments = "Enrollments";
    private const string MemberEnrollments = "MemberEnrollments";

    public override void Down()
    {
        Create.Column("AsDiver").OnTable(MemberEnrollments).AsBoolean().NotNullable().WithDefaultValue(false);
        Delete.Column("AsDiver").FromTable(Enrollments);
    }

    public override void Up()
    {
        Create.Column("AsDiver").OnTable(Enrollments).AsBoolean().NotNullable().WithDefaultValue(false);
        Delete.Column("AsDiver").FromTable(MemberEnrollments);
    }
}