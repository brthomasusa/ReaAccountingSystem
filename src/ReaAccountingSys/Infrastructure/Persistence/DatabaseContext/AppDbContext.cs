using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.Shared;

namespace ReaAccountingSys.Infrastructure.Persistence.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<ExternalAgent>? ExternalAgents { get; set; }
        public DbSet<EconomicEvent>? EconomicEvents { get; set; }
        public DbSet<EconomicResource>? EconomicResources { get; set; }
        public DbSet<DomainUser>? DomainUsers { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<TimeCard>? TimeCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}