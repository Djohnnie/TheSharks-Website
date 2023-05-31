using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

internal class LogConfig : IEntityTypeConfiguration<LogEntry>
{
    public void Configure(EntityTypeBuilder<LogEntry> builder)
    {
        var table = builder.ToTable("_Logs");
        table.Property(x => x.SysId).ValueGeneratedOnAdd().UseIdentityColumn();
        table.HasKey(x => x.Id).IsClustered(false);
        table.HasIndex(x => x.SysId).IsClustered(true);
    }
}