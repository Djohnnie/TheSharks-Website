using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230117102700)]
public class Migration_20230117102700 : Migration
{
    private readonly string AspNetUsers = "AspNetUsers";
    
    public override void Down()
    {
        Alter.Table(AspNetUsers).AlterColumn("Bio").AsString(80).Nullable();
    }

    public override void Up()
    {
        Alter.Table(AspNetUsers).AlterColumn("Bio").AsString(4096).Nullable();
    }
}