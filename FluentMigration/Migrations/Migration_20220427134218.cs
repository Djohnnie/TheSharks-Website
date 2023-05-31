using FluentMigrator;
using System.Data;

namespace TheSharks.FluentMigration.Migrations;

[Migration(20220427134218)]
public class Migration_20220427134218 : Migration
{
    private const string AspNetRoles = "AspNetRoles";
    private const string AspNetUsers = "AspNetUsers";
    private const string AspNetRoleClaims = "AspNetRoleClaims";
    private const string AspNetUserClaims = "AspNetUserClaims";
    private const string AspNetUserLogins = "AspNetUserLogins";
    private const string AspNetUserRoles = "AspNetUserRoles";
    private const string AspNetUserTokens = "AspNetUserTokens";
    private const string Id = "Id";

    public override void Down()
    {
        Delete.Table(AspNetUserRoles);
        Delete.Table(AspNetUserLogins);
        Delete.Table(AspNetRoleClaims);
        Delete.Table(AspNetUserClaims);
        Delete.Table(AspNetUsers);
        Delete.Table(AspNetUserTokens);
        Delete.Table(AspNetRoles);

        Delete.Table("Pictures");
        Delete.Table("Galleries");
        Delete.Table("NewsItems");
        Delete.Table("Components");
        Delete.Table("Pages");
        Delete.Table("Documents");
        Delete.Table("Enrollments");
        Delete.Table("Activities");
    }

    public override void Up()
    {
        Create.Table(AspNetRoles)
            .WithColumn(Id).AsGuid().NotNullable().PrimaryKey("PK_AspNetRoles")
            .WithColumn("ConcernsDivingCertificate").AsBoolean().NotNullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("Name").AsString(256).NotNullable()
            .WithColumn("NormalizedName").AsString(256).Nullable().Indexed("RoleNameIndex");

        Create.Table(AspNetUsers)
            .WithColumn(Id).AsGuid().NotNullable().PrimaryKey("PK_AspNetUsers")
            .WithColumn("FirstName").AsString(60).NotNullable()
            .WithColumn("LastName").AsString(60).NotNullable()
            .WithColumn("Bio").AsString(400).Nullable()
            .WithColumn("ProfilePicture").AsString(int.MaxValue).Nullable()
            .WithColumn("UserName").AsString(256).Nullable().Unique()
            .WithColumn("NormalizedUserName").AsString(256).Nullable().Indexed("UserNameIndex")
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("NormalizedEmail").AsString(256).Nullable().Indexed("EmailIndex")
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("PasswordHash").AsString(int.MaxValue).Nullable()
            .WithColumn("SecurityStamp").AsString(int.MaxValue).Nullable()
            .WithColumn("ConcurrencyStamp").AsString(int.MaxValue).Nullable()
            .WithColumn("PhoneNumber").AsString(30).Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("AccessFailedCount").AsInt32().NotNullable().WithDefaultValue(0);

        Create.Table(AspNetUserTokens)
            .WithColumn(Id).AsGuid().NotNullable().PrimaryKey("PK_AspNetUserTokens")
            .WithColumn("LoginProvider").AsString(450).NotNullable()
            .WithColumn("Name").AsString(450).NotNullable()
            .WithColumn("Value").AsString(int.MaxValue).Nullable()
            .WithColumn(AspNetUsers + Id).AsGuid().NotNullable()
            .ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", AspNetUsers, Id)
            .OnDelete(Rule.Cascade);

        Create.Table(AspNetRoleClaims)
            .WithColumn(Id).AsInt32().PrimaryKey("PK_AspNetRoleClaims").Identity()
            .WithColumn("ClaimType").AsString(int.MaxValue).Nullable()
            .WithColumn("ClaimValue").AsString(int.MaxValue).Nullable()
            .WithColumn("RoleId").AsGuid().NotNullable().Indexed("IX_AspNetRoleClaims_RoleId")
            .ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", AspNetRoles, Id)
            .OnDelete(Rule.Cascade);

        Create.Table(AspNetUserClaims)
            .WithColumn(Id).AsInt32().PrimaryKey("PK_AspNetUserClaims").Identity()
            .WithColumn("ClaimType").AsString(int.MaxValue).Nullable()
            .WithColumn("ClaimValue").AsString(int.MaxValue).Nullable()
            .WithColumn(AspNetUsers + Id).AsGuid().NotNullable().Indexed("IX_AspNetUserClaims_UserId")
            .ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", AspNetUsers, Id)
            .OnDelete(Rule.Cascade);

        Create.Table(AspNetUserLogins)
            .WithColumn("LoginProvider").AsString(450).NotNullable().PrimaryKey("PK_AspNetUserLogins")
            .WithColumn("ProviderKey").AsString(450).NotNullable().PrimaryKey("PK_AspNetUserLogins")
            .WithColumn("ProviderDisplayName").AsString(int.MaxValue).Nullable()
            .WithColumn(AspNetUsers + Id).AsGuid().NotNullable().Indexed("IX_AspNetUserLogins_UserId")
            .ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", AspNetUsers, Id)
            .OnDelete(Rule.Cascade);

        Create.Table(AspNetUserRoles)
            .WithColumn("RoleId").AsGuid().NotNullable().PrimaryKey("PK_AspNetUserRoles")
            .ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", AspNetRoles, Id)
            .OnDelete(Rule.Cascade)
            .WithColumn("UserId").AsGuid().NotNullable().PrimaryKey("PK_AspNetUserRoles").Indexed("IX_AspNetUserRoles_RoleId")
            .ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", AspNetUsers, Id)
            .OnDelete(Rule.Cascade);

        Create.Table("Activities")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("ActivityType").AsString(40).NotNullable()
            .WithColumn("Name").AsString(120).NotNullable()
            .WithColumn("Date").AsDateTime().NotNullable()
            .WithColumn("Location").AsString(255).NotNullable()
            .WithColumn("Info").AsString(500).Nullable()
            .WithColumn("MemberInfo").AsString(500).Nullable()
            .WithColumn("NecessarySubscription").AsBoolean().NotNullable()
            .WithColumn("Departure").AsDateTime().Nullable().WithDefaultValue(null)
            .WithColumn("StartTime").AsDateTime().Nullable().WithDefaultValue(null)
            .WithColumn("EndTime").AsDateTime().Nullable().WithDefaultValue(null)
            .WithColumn("BriefingTime").AsDateTime().Nullable().WithDefaultValue(null)
            .WithColumn("Tide").AsString(50).Nullable().WithDefaultValue(null)
            .WithColumn("AtWater").AsDateTime().Nullable().WithDefaultValue(null)
            .WithColumn("ResponsibleId").AsGuid().Nullable().Indexed("IX_Activities_ResponsibleId") // Nullable, Kan later toegewezen worden?
            .ForeignKey("FK_Activities_AspNetUsers_ResponsibleId", AspNetUsers, Id)
            .OnDelete(Rule.SetNull);

        Create.Table("NewsItems")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Title").AsString(60).NotNullable()
            .WithColumn("Content").AsString(1000).NotNullable()
            .WithColumn("PublishDate").AsDateTime()
            .WithColumn("AuthorId").AsGuid().Nullable().Indexed("IX_NewsItems_AuthorId")
            .ForeignKey("FK_NewsItems_AspNetUsers_AuthorId", AspNetUsers, Id)
            .OnDelete(Rule.SetNull); // Moet een newsItem verwijderd worden als Member eruit gesmeten worden?

        Create.Table("Galleries")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Name").AsString(60).NotNullable();

        Create.Table("Pictures")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Name").AsString(120).NotNullable()
            .WithColumn("Url").AsString(255).NotNullable()
            .WithColumn("StorageUrl").AsString(300).NotNullable()
            .WithColumn("GalleryId").AsGuid().NotNullable().Indexed("IX_Pictures_GalleryId")
            .ForeignKey("FK_Pictures_Galleries_GalleryId", "Galleries", Id)
            .OnDelete(Rule.Cascade);

        Create.Table("Documents")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Name").AsString(120).NotNullable()
            .WithColumn("Topic").AsString(120).NotNullable()
            .WithColumn("Url").AsString(300).NotNullable()
            .WithColumn("TimesDownloaded").AsInt16().NotNullable();

        Create.Table("Pages")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Title").AsString(30).NotNullable()
            .WithColumn("Link").AsString(300).NotNullable().Unique()
            .WithColumn("NavBarPosition").AsInt32().NotNullable()
            .WithColumn("NavBarSubPosition").AsInt32().NotNullable();

        Create.Table("Components")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Name").AsString(30).NotNullable()
            .WithColumn("JsonStructure").AsString(8000).NotNullable()
            .WithColumn("WebsiteHierarchyPosition").AsInt32().NotNullable()
            .WithColumn("PageId").AsGuid().NotNullable().Indexed("IX_Components_PageId")
            .ForeignKey("FK_Components_Pages_PageId", "Pages", Id)
            .OnDelete(Rule.Cascade);

        Create.Table("Enrollments")
            .WithColumn("Id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("Remark").AsString(200).Nullable()
            .WithColumn("RegistratorId").AsGuid().NotNullable().Indexed("IX_Enrollments_MemberId")
            .ForeignKey("FK_Enrollments_AspNetUsers_MemberId", AspNetUsers, Id)
            .WithColumn("ActivityId").AsGuid().NotNullable().Indexed("IX_Enrollments_ActivityId")
            .ForeignKey("FK_Enrollments_Activities_ActivityId", "Activities", Id)
            .OnDelete(Rule.Cascade);
    }
}
