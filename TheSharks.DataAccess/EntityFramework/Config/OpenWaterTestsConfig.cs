using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

internal class OpenWaterTestsConfig : IEntityTypeConfiguration<OpenWaterTest>
{
    public void Configure(EntityTypeBuilder<OpenWaterTest> builder)
    {
        var table = builder.ToTable("OpenWaterTests");
        table.Property(x => x.SysId).ValueGeneratedOnAdd().UseIdentityColumn();
        table.HasKey(x => x.Id).IsClustered(false);
        table.HasIndex(x => x.SysId).IsClustered(true);
    }
}