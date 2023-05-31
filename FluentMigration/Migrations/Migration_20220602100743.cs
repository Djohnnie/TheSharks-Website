using FluentMigrator;
using TheSharks.FluentMigration.Seed;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220602100743)]
public class Migration_20220602100743 : Migration
{
    private const string Documents = "Documents";
    private readonly bool _seed;

    public Migration_20220602100743(MigrationRunnerOptions options)
    {
        _seed = options.Seed;
    }

    public override void Down()
    {
        Delete.Column("FileName").FromTable(Documents);
        Alter.Column("TimesDownloaded").OnTable(Documents).AsInt16().NotNullable();
    }

    public override void Up()
    {
        Create.Column("FileName").OnTable(Documents).AsString().NotNullable();
        Alter.Column("TimesDownloaded").OnTable(Documents).AsInt32().NotNullable();

        if (_seed)
        {
            this.SeedData();
        }
    }
}
