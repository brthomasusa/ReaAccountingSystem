using System;
using System.IO;
using FluentValidation;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;
using LoggingService.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using ReaAccountingSys.Shared;
using ReaAccountingSys.Infrastructure;

using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Server.Extensions;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

var builder = WebApplication.CreateBuilder(args);

var infrastructureAssembly = typeof(ReaAccountingSys.Infrastructure.AssembleReference).Assembly;
var sharedAssembly = typeof(ReaAccountingSys.Shared.AssemblyReference).Assembly;

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
builder.Services.ConfigureLoggerService();
builder.Services.AddInfrastructureServices();
builder.Services.ConfigureEfCoreDbContext(builder.Configuration);
builder.Services.ConfigureDapper(builder.Configuration);
builder.Services.AddRepositoryServices();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

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

app.Run();

public partial class Program { }

