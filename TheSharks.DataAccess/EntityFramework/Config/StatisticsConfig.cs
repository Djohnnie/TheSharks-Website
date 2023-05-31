using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class StatisticsConfig : IEntityTypeConfiguration<Statistics>
{
    public void Configure(EntityTypeBuilder<Statistics> builder)
    {
        var table = builder.ToTable("Statistics");
        table.Property(x => x.SysId).ValueGeneratedOnAdd().UseIdentityColumn();
        table.HasKey(x => x.Id).IsClustered(false);
        table.HasIndex(x => x.SysId).IsClustered(true);
    }
}