using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class DiveActivityConfig : IEntityTypeConfiguration<DiveActivity>
{
    public void Configure(EntityTypeBuilder<DiveActivity> builder)
    {
        builder.Property(p => p.Departure).HasColumnName("Departure");
    }
}