using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects;
using ReaAccountingSys.Core.Shared;
using ReaAccountingSys.SharedKernel.CommonValueObjects;

namespace ReaAccountingSys.Infrastructure.Persistence.Configuration.HumanResources
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees", schema: "HumanResources");
            entity.HasKey(e => e.Id);
            entity.HasMany<TimeCard>(p => p.TimeCards).WithOne().HasForeignKey(p => p.EmployeeId);

            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("EmployeeId").ValueGeneratedNever();
            entity.Property(p => p.EmployeeType).HasColumnType("INT").HasColumnName("EmployeeTypeId").IsRequired();
            entity.Property(p => p.SupervisorId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("SupervisorId")
                .IsRequired();
            entity.OwnsOne(p => p.EmployeeName, p =>
            {
                p.Property(pp => pp.LastName).HasColumnType("NVARCHAR(25)").HasColumnName("LastName").IsRequired();
                p.Property(pp => pp.FirstName).HasColumnType("NVARCHAR(25)").HasColumnName("FirstName").IsRequired();
                p.Property(pp => pp.MiddleInitial).HasColumnType("NCHAR(1)").HasColumnName("MiddleInitial").IsRequired(false);
            });
            entity.Property(p => p.SSN)
                .HasConversion(p => p.Value, p => SocialSecurityNumber.Create(p!))
                .HasColumnType("NVARCHAR(9)")
                .HasColumnName("SSN")
                .IsRequired();
            entity.Property(p => p.EmailAddress)
                .HasConversion(p => p.Value, p => EmailAddress.Create(p!))
                .HasColumnType("NVARCHAR(256)")
                .HasColumnName("EmailAddress")
                .IsRequired();
            entity.Property(p => p.EmployeeTelephone)
                .HasConversion(p => p.Value, p => PhoneNumber.Create(p!))
                .HasColumnType("NVARCHAR(14)")
                .HasColumnName("Telephone")
                .IsRequired();
            entity.OwnsOne(p => p.EmployeeAddress, p =>
            {
                p.Property(pp => pp.AddressLine1).HasColumnType("NVARCHAR(30)").HasColumnName("AddressLine1").IsRequired();
                p.Property(pp => pp.AddressLine2).HasColumnType("NVARCHAR(30)").HasColumnName("AddressLine2").IsRequired(false);
                p.Property(pp => pp.City).HasColumnType("NVARCHAR(30)").HasColumnName("City").IsRequired();
                p.Property(pp => pp.StateCode).HasColumnType("NCHAR(2)").HasColumnName("StateCode").IsRequired();
                p.Property(pp => pp.Zipcode).HasColumnType("NVARCHAR(10)").HasColumnName("Zipcode").IsRequired();
            });
            entity.Property(p => p.MaritalStatus)
                .HasConversion(p => p.Value, p => MaritalStatus.Create(p!))
                .HasColumnType("NCHAR(1)")
                .HasColumnName("MaritalStatus")
                .IsRequired();
            entity.Property(p => p.TaxExemptions)
                .HasConversion(p => p.Value, p => TaxExemption.Create(p))
                .HasColumnType("int")
                .HasColumnName("Exemptions")
                .IsRequired();
            entity.Property(p => p.EmployeePayRate)
                .HasConversion(p => p.Value, p => PayRate.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("PayRate")
                .IsRequired();
            entity.Property(p => p.EmploymentDate)
                .HasConversion(p => p.Value, p => StartDate.Create(p))
                .HasColumnType("datetime2(0)")
                .HasColumnName("StartDate")
                .IsRequired();
            entity.Property(p => p.IsActive)
                .HasColumnType("BIT")
                .HasColumnName("IsActive")
                .IsRequired();
            entity.Property(p => p.IsSupervisor)
                .HasColumnType("BIT")
                .HasColumnName("IsSupervisor")
                .IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}