using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220729113900)]
public class Migration_20220729113900 : Migration
{
    private const string Activities = "Activities";

    public override void Down()
    {
        Delete.Column("LocationLink").FromTable(Activities);
    }

    public override void Up()
    {
        Create.Column("LocationLink").OnTable(Activities).AsString(30).WithDefaultValue("").NotNullable();
    }
}