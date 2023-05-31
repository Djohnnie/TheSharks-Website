using FluentMigrator;
using FluentMigrator.SqlServer;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230101114500)]
public class Migration_20230101114500 : Migration
{
    private const string Statistics = "Statistics";

    public override void Down()
    {
        Delete.Table(Statistics);
    }

    public override void Up()
    {
        Create.Table(Statistics)
            .WithColumn("Id").AsGuid().NotNullable()
            .WithColumn("SysId").AsInt64().Identity().NotNullable()
            .WithColumn("Page").AsString(256).NotNullable()
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("IsLoggedIn").AsBoolean().NotNullable();

        Create.PrimaryKey("PK_Statistics").OnTable(Statistics).Column("Id").NonClustered();
        Create.UniqueConstraint().OnTable(Statistics).Column("SysId").Clustered();
        Create.Index("IX_Statistics").OnTable(Statistics).OnColumn("Date").Ascending();
    }
}