using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220523162527)]
public class Migration_20220523162527 : Migration
{
    private const string Galleries = "Galleries";
    private const string Pictures = "Pictures";
    private const string Documents = "Documents";

    public override void Down()
    {
        Delete.Column("AmountPictures").FromTable(Galleries);
        Delete.Column("UrlFirstPicture").FromTable(Galleries);
        Delete.Column("CreationDate").FromTable(Galleries);
        Delete.Column("CreationDate").FromTable(Documents);

        Alter.Table(Pictures).AddColumn("Url").AsString(255).NotNullable();
    }

    public override void Up()
    {
        Alter.Table(Galleries).AddColumn("AmountPictures").AsInt32().NotNullable().WithDefaultValue(0);
        Alter.Table(Galleries).AddColumn("UrlFirstPicture").AsString().Nullable();
        Alter.Table(Galleries).AddColumn("CreationDate").AsDateTimeOffset().NotNullable();
        Alter.Table(Documents).AddColumn("CreationDate").AsDateTimeOffset().NotNullable();

        Delete.Column("Url").FromTable(Pictures);
    }
}
