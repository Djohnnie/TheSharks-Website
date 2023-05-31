using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230112151700)]
public class Migration_20230112151700 : Migration
{
    private const string NewsItems = "NewsItems";

    public override void Down()
    {
        Delete.Column("MembersOnly").FromTable(NewsItems);
    }

    public override void Up()
    {
        Create.Column("MembersOnly").OnTable(NewsItems).AsBoolean().NotNullable().WithDefaultValue(false);
    }
}