using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReaAccountingSys.Core.Shared;
// using ReaAccountingSys.Core.CashManagement.CashAccountAggregate;
// using ReaAccountingSys.Core.Financing.LoanAgreementAggregate;
// using ReaAccountingSys.Core.Financing.StockSubscriptionAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;

namespace ReaAccountingSys.Infrastructure.Persistence.Configuration.Shared
{
    internal class EconomicEventConfig : IEntityTypeConfiguration<EconomicEvent>
    {
        public void Configure(EntityTypeBuilder<EconomicEvent> entity)
        {
            entity.ToTable("EconomicEvents", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.HasOne<TimeCard>().WithOne().HasForeignKey<TimeCard>("Id");

            // entity.HasOne<CashTransfer>().WithOne().HasForeignKey<CashTransfer>("Id");
            //TODO Enable entity.HasOne<CashTransfer>() after adding CashTransfer to dbcontext
            // entity.HasOne<LoanAgreement>().WithOne().HasForeignKey<LoanAgreement>("Id");
            //TODO Enable entity.HasOne<LoanAgreement>() after adding LoanAgreement to dbcontext
            // entity.HasOne<LoanInstallment>().WithOne().HasForeignKey<LoanInstallment>("Id");
            //TODO Enable entity.HasOne<LoanInstallment>() after adding LoanInstallment to dbcontext
            // entity.HasOne<StockSubscription>().WithOne().HasForeignKey<StockSubscription>("Id");
            //TODO Enable entity.HasOne<StockSubscription>() after adding StockSubscription to dbcontext
            // entity.HasOne<DividendDeclaration>().WithOne().HasForeignKey<DividendDeclaration>("Id");
            //TODO Enable entity.HasOne<DividendDeclaration>() after adding DividendDeclaration to dbcontext



            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("EventId").ValueGeneratedNever();
            entity.Property(p => p.EventType).HasColumnType("int").HasColumnName("EventTypeId").IsRequired();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}