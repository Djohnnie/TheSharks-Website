using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220525161954)]
public class Migration_20220525161954 : Migration
{
    private readonly string AspNetUsers = "AspNetUsers";
    private readonly string Activities = "Activities";
    private readonly string Documents = "Documents";
    private readonly string GuestEnrollments = "GuestEnrollments";
    private readonly string Galleries = "Galleries";
    private readonly string Pages = "Pages";

    public override void Down()
    {
        Alter.Table(Activities).AlterColumn("Location").AsString(255);
        Alter.Table(Activities).AlterColumn("Info").AsString(500).Nullable();
        Alter.Table(Activities).AlterColumn("MemberInfo").AsString(500).Nullable();

        Alter.Table(Documents).AlterColumn("Topic").AsString(120);

        Alter.Table(GuestEnrollments).AlterColumn("Registree").AsString(400);
        Alter.Table(GuestEnrollments).AlterColumn("DiveLevel").AsString(30);

        Alter.Table(Galleries).AlterColumn("Name").AsString(60);

        Alter.Table(AspNetUsers).AlterColumn("FirstName").AsString(60);
        Alter.Table(AspNetUsers).AlterColumn("LastName").AsString(60);
        Alter.Table(AspNetUsers).AlterColumn("Bio").AsString(400).Nullable();

        Delete.Index("IX_Pages_Link").OnTable(Pages);
        Alter.Column("Link").OnTable(Pages).AsString(300).Unique();
    }

    public override void Up()
    {
        Alter.Table(Activities).AlterColumn("Location").AsString(40);
        Alter.Table(Activities).AlterColumn("Info").AsString(150).Nullable();
        Alter.Table(Activities).AlterColumn("MemberInfo").AsString(150).Nullable();

        Alter.Table(Documents).AlterColumn("Topic").AsString(50);

        Alter.Table(GuestEnrollments).AlterColumn("Registree").AsString(60);
        Alter.Table(GuestEnrollments).AlterColumn("DiveLevel").AsString(20);

        Alter.Table(Galleries).AlterColumn("Name").AsString(40);

        Alter.Table(AspNetUsers).AlterColumn("FirstName").AsString(30);
        Alter.Table(AspNetUsers).AlterColumn("LastName").AsString(30);
        Alter.Table(AspNetUsers).AlterColumn("Bio").AsString(80).Nullable();

        Delete.Index("IX_Pages_Link").OnTable(Pages);
        Alter.Column("Link").OnTable(Pages).AsString(120).Unique();
    }
}
