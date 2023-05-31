using FluentMigrator;
using System.Data;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230126160000)]
public class Migration_20230126160000 : Migration
{
    private const string OpenWaterTests = "OpenWaterTests";

    public override void Down()
    {
        Alter.Column("Title").OnTable(OpenWaterTests).AsString(80).NotNullable();
    }

    public override void Up()
    {
        Alter.Column("Title").OnTable(OpenWaterTests).AsString(120).NotNullable();
    }
}