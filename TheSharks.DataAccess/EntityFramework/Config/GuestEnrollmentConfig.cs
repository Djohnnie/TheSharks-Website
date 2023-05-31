using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class GuestEnrollmentConfig : IEntityTypeConfiguration<GuestEnrollment>
{
    public void Configure(EntityTypeBuilder<GuestEnrollment> builder)
    {
        builder.ToTable("GuestEnrollments");
    }
}