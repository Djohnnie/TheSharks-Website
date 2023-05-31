using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework;

public class TheSharksContext : IdentityDbContext<Member, Role, Guid, MemberClaim, MemberRole, MemberLogin, RoleClaim, MemberToken>, IDataProtectionKeyContext
{
    public TheSharksContext(DbContextOptions<TheSharksContext> options) : base(options) { }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<BoardMeetingActivity> BoardMeetingActivities { get; set; }
    public DbSet<DiveActivity> DiveActivities { get; set; }
    public DbSet<EventActivity> EventActivities { get; set; }
    public DbSet<MonitorBoardActivity> MonitorBoardActivities { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<MemberEnrollment> MemberEnrollments { get; set; }
    public DbSet<GuestEnrollment> GuestEnrollments { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<NewsItem> NewsItems { get; set; }
    public DbSet<OpenWaterTest> OpenWaterTests { get; set; }
    public DbSet<Statistics> Statistics { get; set; }
    public DbSet<LogEntry> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheSharksContext).Assembly);

        modelBuilder.Entity<MemberRole>(x =>
        {
            x.HasKey(ur => new { ur.UserId, ur.RoleId });

            x.HasOne(ur => ur.Role)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            x.HasOne(ur => ur.Member)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });
    }
}