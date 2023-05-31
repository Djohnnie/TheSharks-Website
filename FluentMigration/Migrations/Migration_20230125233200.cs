using FluentMigrator;
using System.Data;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20230125233200)]
public class Migration_20230125233200 : Migration
{
    private const string Enrollments = "Enrollments";

    public override void Down()
    {
        Delete.ForeignKey("FK_Enrollments_OpenWaterTests_OpenWaterTestId").OnTable(Enrollments);
        Delete.Column("OpenWaterTestId").FromTable(Enrollments);
    }

    public override void Up()
    {
        Alter.Table(Enrollments)
            .AddColumn("OpenWaterTestId").AsGuid().Nullable().Indexed("IX_Enrollments_OpenWaterTestId")
            .ForeignKey("FK_Enrollments_OpenWaterTests_OpenWaterTestId", "OpenWaterTests", "Id")
            .OnDelete(Rule.SetNull);
    }
}