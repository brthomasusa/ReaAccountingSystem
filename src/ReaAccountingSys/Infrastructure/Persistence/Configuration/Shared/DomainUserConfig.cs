using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReaAccountingSys.Core.Shared;

namespace ReaAccountingSys.Infrastructure.Persistence.Configuration.Shared
{
    internal class DomainUserConfig : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> entity)
        {
            entity.ToTable("DomainUsers", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.HasOne<ExternalAgent>().WithOne().HasForeignKey<DomainUser>(e => e.Id);

            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("UserId").ValueGeneratedNever();
            entity.Property(p => p.UserName).HasColumnType("NVARCHAR(256)").HasColumnName("UserName");
            entity.Property(p => p.Email).HasColumnType("NVARCHAR(256)").HasColumnName("Email");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}