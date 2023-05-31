using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class ActivityConfig : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities")
            .HasDiscriminator<string>("ActivityType")
            .HasValue<Activity>("base_activity")
            .HasValue<BoardMeetingActivity>("boardmeeting")
            .HasValue<DiveActivity>("dive")
            .HasValue<EventActivity>("event")
            .HasValue<MonitorBoardActivity>("monitorboard");
    }
}