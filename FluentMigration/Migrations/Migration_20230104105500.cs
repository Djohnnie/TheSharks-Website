using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230104105500)]
public class Migration_20230104105500 : Migration
{
    private const string Statistics = "Statistics";

    public override void Down()
    {
        Delete.Column("SessionId").FromTable(Statistics);
    }

    public override void Up()
    {
        Create.Column("SessionId").OnTable(Statistics).AsGuid().NotNullable().WithDefaultValue(Guid.Empty);
    }
}