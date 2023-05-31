using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220726144200)]
public class Migration_20220726144200 : Migration
{
    private const string Documents = "Documents";

    public override void Down()
    {
        Create.Column("TimesDownloaded").OnTable(Documents).AsInt32().NotNullable().WithDefaultValue(0);
        Create.Column("Topic").OnTable(Documents).AsString(50).NotNullable().WithDefaultValue("unknown");
        Delete.Column("IsImportant").FromTable(Documents);
    }

    public override void Up()
    {
        Delete.Column("TimesDownloaded").FromTable(Documents);
        Delete.Column("Topic").FromTable(Documents);
        Create.Column("IsImportant").OnTable(Documents).AsBoolean().NotNullable().WithDefaultValue(false);
    }
}