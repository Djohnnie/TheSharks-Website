using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230123095200)]
public class Migration_20230123095200 : Migration
{
    private const string DataProtectionKeys = "DataProtectionKeys";

    public override void Down()
    {
        Delete.Table(DataProtectionKeys);
    }

    public override void Up()
    {
        Create.Table(DataProtectionKeys)
            .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey("PK_DataProtectionKeys")
            .WithColumn("FriendlyName").AsString(int.MaxValue).Nullable()
            .WithColumn("Xml").AsString(int.MaxValue).Nullable();
    }
}