using FluentValidation;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;
// using Microsoft.AspNetCore.ResponseCompression;
using ReaAccountingSys.Server.Interceptors;
using ReaAccountingSys.Server.Extensions;
using ReaAccountingSys.Server.GrpcServices.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{

    var builder = WebApplication.CreateBuilder(args);

    var infrastructureAssembly = typeof(ReaAccountingSys.Infrastructure.AssembleReference).Assembly;
    var sharedAssembly = typeof(ReaAccountingSys.Shared.AssemblyReference).Assembly;

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddGrpc(options =>
    {
        options.EnableDetailedErrors = true;
        options.MaxReceiveMessageSize = 6291456; // 6 MB
        options.MaxSendMessageSize = 6291456; // 6 MB
        options.CompressionProviders = new List<ICompressionProvider>
        {
            new BrotliCompressionProvider() // br
        };
        options.ResponseCompressionAlgorithm = "br"; // grpc-accept-encoding
        options.ResponseCompressionLevel = CompressionLevel.Optimal; // compression level used if not set on the provider
        options.Interceptors.Add<ExceptionInterceptor>(); // Register custom ExceptionInterceptor interceptor
    });
    builder.Services.AddGrpcReflection();


    builder.Services.AddValidatorsFromAssemblyContaining<EmployeeWriteModelValidator>();
    // builder.Services.AddScoped<IValidator<EmployeeWriteModel>, EmployeeWriteModelValidator>();

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
    });

    // Add services from namespace Server.Extensions to the container.
    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.AddInfrastructureServices();
    builder.Services.ConfigureEfCoreDbContext(builder.Configuration);
    builder.Services.ConfigureDapper(builder.Configuration);
    builder.Services.AddRepositoryServices();

    var app = builder.Build();

    app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
    app.MapGrpcReflectionService();
    app.MapGrpcService<EmployeeGrpcService>();
    // app.MapGrpcService<EmployeeGrpcServiceBrowser>();

    // app.ConfigureExceptionHandler(logger);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        // The default HSTS value is 30 days. You may want to change this 
        // for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.All
    });

    app.UseCors("CorsPolicy");
    app.UseRouting();

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.MapGet("/", () =>
        "Communication with gRPC endpoints must be made through a gRPC client."
    );

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

public partial class Program { }

