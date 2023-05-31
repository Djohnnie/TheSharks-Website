using FluentMigrator;
using System.Data;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220504120533)]
public class Migration_20220504120533 : Migration
{
    private const string AspNetUsers = "AspNetUsers";
    private const string Activities = "Activities";
    private const string NewsItems = "NewsItems";
    private const string Id = "Id";

    public override void Down()
    {
        Alter.Table(AspNetUsers).AlterColumn("ProfilePicture").AsString(int.MaxValue);

        Alter.Table(Activities).AlterColumn("Date").AsDateTime().NotNullable();
        Alter.Table(Activities).AlterColumn("Departure").AsDateTime().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AlterColumn("StartTime").AsDateTime().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AlterColumn("EndTime").AsDateTime().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AlterColumn("BriefingTime").AsDateTime().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AlterColumn("AtWater").AsDateTime().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AlterColumn("ResponsibleId").AsGuid().Nullable().Indexed("IX_Activities_ResponsibleId")
            .ForeignKey("FK_Activities_AspNetUsers_ResponsibleId", AspNetUsers, Id)
            .OnDelete(Rule.SetNull);

        Alter.Table(NewsItems).AlterColumn("PublishDate").AsDateTime();
    }

    public override void Up()
    {
        Delete.Column("ProfilePicture").FromTable(AspNetUsers);
        Alter.Table(AspNetUsers).AddColumn("ProfilePicture").AsBinary(int.MaxValue).Nullable();

        Delete.Column("Date").FromTable(Activities);
        Delete.Column("Departure").FromTable(Activities);
        Delete.Column("StartTime").FromTable(Activities);
        Delete.Column("EndTime").FromTable(Activities);
        Delete.Column("BriefingTime").FromTable(Activities);
        Delete.Column("AtWater").FromTable(Activities);
        Delete.ForeignKey("FK_Activities_AspNetUsers_ResponsibleId").OnTable(Activities);
        Delete.Index("IX_Activities_ResponsibleId").OnTable(Activities);
        Delete.Column("ResponsibleId").FromTable(Activities);
        Alter.Table(Activities).AddColumn("Date").AsDateTimeOffset().Nullable();
        Alter.Table(Activities).AddColumn("Departure").AsDateTimeOffset().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AddColumn("StartTime").AsDateTimeOffset().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AddColumn("EndTime").AsDateTimeOffset().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AddColumn("BriefingTime").AsDateTimeOffset().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AddColumn("AtWater").AsDateTimeOffset().Nullable().WithDefaultValue(null);
        Alter.Table(Activities).AddColumn("ResponsibleId").AsGuid().NotNullable().Indexed("IX_Activities_ResponsibleId")
           .ForeignKey("FK_Activities_AspNetUsers_ResponsibleId", AspNetUsers, Id)
           .OnDelete(Rule.Cascade);

        Delete.Column("PublishDate").FromTable(NewsItems);
        Alter.Table(NewsItems).AddColumn("PublishDate").AsDateTimeOffset();
    }
}
