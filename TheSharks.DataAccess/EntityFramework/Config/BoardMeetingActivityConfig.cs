using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class BoardMeetingActivityConfig : IEntityTypeConfiguration<BoardMeetingActivity>
{
    public void Configure(EntityTypeBuilder<BoardMeetingActivity> builder)
    {
        builder.Property(p => p.StartTime).HasColumnName("StartTime");
        builder.Property(p => p.EndTime).HasColumnName("EndTime");
    }
}