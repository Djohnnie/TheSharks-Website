using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSharks.Domain.Entities;

namespace TheSharks.DataAccess.EntityFramework.Config;

public class MemberEnrollmentConfig : IEntityTypeConfiguration<MemberEnrollment>
{
    public void Configure(EntityTypeBuilder<MemberEnrollment> builder)
    {
        builder.ToTable("MemberEnrollments");
    }
}