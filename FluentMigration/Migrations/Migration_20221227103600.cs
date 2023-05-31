using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20221227103600)]
public class Migration_20221227103600 : Migration
{
    private const string GuestEnrollments = "GuestEnrollments";

    public override void Down()
    {
        Create.Column("GuestType").OnTable(GuestEnrollments).AsString(30).NotNullable();
    }

    public override void Up()
    {
        Delete.Column("GuestType").FromTable(GuestEnrollments);
    }
}