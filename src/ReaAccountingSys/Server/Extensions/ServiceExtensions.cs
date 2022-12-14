using ReaAccountingSys.Application.Handlers.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.Infrastructure.Persistence.Repositories;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Interfaces;

namespace ReaAccountingSys.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterDomainEventHandlers(this IServiceCollection services)
        {
            var serviceProvider = new ServiceCollection()
                .Scan(scan => scan
                    .FromAssemblyOf<GroupMgrChangedEventHandler>()
                    .AddClasses(classes =>
                        classes.AssignableTo(typeof(IDomainEventHandler<>)))
                    .AsImplementedInterfaces()
                ).BuildServiceProvider();

            DomainEventDispatcher._serviceProvider = serviceProvider;

            return services;
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "validation-errors-text"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

        // public static void ConfigureLoggerService(this IServiceCollection services) =>
        //     services.AddSingleton<ILoggerManager, LoggerManager>();

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
            .AddScoped<IWriteRepositoryManager, WriteRepositoryManager>()
            .AddScoped<IReadRepositoryManager, ReadRepositoryManager>();

            // .AddScoped<IFinancierAggregateRepository, FinancierAggregateRepository>()
            // .AddScoped<ILoanAgreementAggregateRepository, LoanAgreementAggregateRepository>()
            // .AddScoped<ICashAccountAggregateRepository, CashAccountAggregateRepository>()
            // .AddScoped<IStockSubscriptionAggregateRepository, StockSubscriptionAggregateRepository>();
        }
    }
}