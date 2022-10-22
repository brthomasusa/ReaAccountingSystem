using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FluentValidation;
using Fluxor;

using ReaAccountingSys.Client;
using ReaAccountingSys.Client.Services.Fluxor.HumanResources;

var currentAssembly = typeof(Program).Assembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddBlazorise(options =>
 {
     options.Immediate = true;
 })
  .AddBootstrap5Providers()
  .AddFontAwesomeIcons();

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(currentAssembly);
#if DEBUG
    options.UseReduxDevTools();
#endif        
});
builder.Services.AddScoped<EmployeeAggregateStateFacade>();
builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddSingleton(services =>
{
    var navigationManager = services.GetRequiredService<NavigationManager>();
    var backendUrl = navigationManager.BaseUri;

    // Create a channel with a GrpcWebHandler that is addressed to the backend server.
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
});

await builder.Build().RunAsync();
