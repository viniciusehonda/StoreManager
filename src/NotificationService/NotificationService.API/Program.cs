
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NotificationService.Application;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Notifications;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

await app.RunAsync();

namespace NotificationService.Api
{
    public partial class Program;
}
