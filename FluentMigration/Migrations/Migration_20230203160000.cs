using FluentMigrator;
using FluentMigrator.SqlServer;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230203160000)]
public class Migration_20230203160000 : Migration
{
    private const string Logs = "_Logs";

    public override void Down()
    {
        Delete.Table(Logs);
    }

    public override void Up()
    {
        Create.Table(Logs)
            .WithColumn("Id").AsGuid().NotNullable()
            .WithColumn("SysId").AsInt64().Identity().NotNullable()
            .WithColumn("Date").AsDateTime2().NotNullable()
            .WithColumn("User").AsString(128).Nullable()
            .WithColumn("Identifier").AsString(128).Nullable()
            .WithColumn("Message").AsString(1024).Nullable()
            .WithColumn("Data").AsString(int.MaxValue).Nullable();

        Create.PrimaryKey("PK_Logs").OnTable(Logs).Column("Id").NonClustered();
        Create.UniqueConstraint().OnTable(Logs).Column("SysId").Clustered();
    }
}