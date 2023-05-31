using FluentMigrator;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220520155225)]
public class Migration_20220520155225 : Migration
{
    public override void Down()
    {
        Rename.Column("Content").OnTable("Components").To("JSONStructure");
        Rename.Column("Position").OnTable("Components").To("WebsiteHierarchyPosition");
    }

    public override void Up()
    {
        Rename.Column("JSONStructure").OnTable("Components").To("Content");
        Rename.Column("WebsiteHierarchyPosition").OnTable("Components").To("Position");
    }
}
