using System.Text;
using Microsoft.EntityFrameworkCore;

using ReaAccountingSys.Core.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.Infrastructure.Persistence.Repositories;
using ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources;
using LoggingService;
using LoggingService.Interfaces;

namespace ReaAccountingSys.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureEfCoreDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration["ConnectionStrings:DefaultConnection"],
                    msSqlOptions => msSqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseLazyLoadingProxies()
            );
        }

        public static void ConfigureDapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>(s => new DapperContext(configuration["ConnectionStrings:DefaultConnection"]));
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, AppUnitOfWork>();
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IEmployeeAggregateRepository, EmployeeAggregateRepository>();
            // .AddScoped<IFinancierAggregateRepository, FinancierAggregateRepository>()
            // .AddScoped<ILoanAgreementAggregateRepository, LoanAgreementAggregateRepository>()
            // .AddScoped<ICashAccountAggregateRepository, CashAccountAggregateRepository>()
            // .AddScoped<IStockSubscriptionAggregateRepository, StockSubscriptionAggregateRepository>();
        }
    }
}