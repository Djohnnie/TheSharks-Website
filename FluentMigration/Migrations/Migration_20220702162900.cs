using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220702162900)]
public class Migration_20220702162900 : Migration
{
    private const string Pages = "Pages";

    public override void Down()
    {
        Delete.Column("IsOnlyAvailableForMembers").FromTable(Pages);
        Delete.Column("IsDefaultPage").FromTable(Pages);
        Delete.Column("IsDefaultPageForMembers").FromTable(Pages);
    }

    public override void Up()
    {
        Create.Column("IsOnlyAvailableForMembers").OnTable(Pages).AsBoolean().NotNullable().WithDefaultValue(false);
        Create.Column("IsDefaultPage").OnTable(Pages).AsBoolean().NotNullable().WithDefaultValue(false);
        Create.Column("IsDefaultPageForMembers").OnTable(Pages).AsBoolean().NotNullable().WithDefaultValue(false);
    }
}