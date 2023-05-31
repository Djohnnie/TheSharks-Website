using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220519141313)]
public class Migration_20220519141313 : Migration
{
    public override void Down()
    {
        Alter.Table("NewsItems").AlterColumn("Content").AsString(1000);
    }

    public override void Up()
    {
        Alter.Table("NewsItems").AlterColumn("Content").AsString(int.MaxValue);
    }
}
