using FluentMigrator;
using FluentMigrator.SqlServer;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230125101900)]
public class Migration_20230125101900 : Migration
{
    private const string OpenWaterTests = "OpenWaterTests";

    public override void Down()
    {
        Delete.Table(OpenWaterTests);
    }

    public override void Up()
    {
        Create.Table(OpenWaterTests)
            .WithColumn("Id").AsGuid().NotNullable()
            .WithColumn("SysId").AsInt64().Identity().NotNullable()
            .WithColumn("Title").AsString(80).NotNullable()
            .WithColumn("DiveCertificate").AsString(30).NotNullable()
            .WithColumn("Content").AsString(int.MaxValue).NotNullable();

        Create.PrimaryKey("PK_OpenWaterTests").OnTable(OpenWaterTests).Column("Id").NonClustered();
        Create.UniqueConstraint().OnTable(OpenWaterTests).Column("SysId").Clustered();
    }
}