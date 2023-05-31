using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230111094700)]
public class Migration_20230111094700 : Migration
{
    private const string Statistics = "Statistics";

    public override void Down()
    {
        Delete.Column("IsApp").FromTable(Statistics);
    }

    public override void Up()
    {
        Create.Column("IsApp").OnTable(Statistics).AsBoolean().NotNullable().WithDefaultValue(false);
    }
}