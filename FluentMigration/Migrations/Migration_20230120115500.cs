using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230120115500)]
public class Migration_20230120115500 : Migration
{
    private const string Activities = "Activities";

    public override void Down()
    {
        Alter.Table(Activities).AlterColumn("LocationLink").AsString(30).WithDefaultValue("").NotNullable();
    }

    public override void Up()
    {
        Alter.Table(Activities).AlterColumn("LocationLink").AsString(256).Nullable();
    }
}