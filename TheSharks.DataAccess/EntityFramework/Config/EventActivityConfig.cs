using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class EventActivityConfig : IEntityTypeConfiguration<EventActivity>
{
    public void Configure(EntityTypeBuilder<EventActivity> builder)
    {
        builder.Property(p => p.Departure).HasColumnName("Departure");
        builder.Property(p => p.StartTime).HasColumnName("StartTime");
        builder.Property(p => p.EndTime).HasColumnName("EndTime");
    }
}