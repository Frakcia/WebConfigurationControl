using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebConfigurationControl.Application;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;
using WebConfigurationControl.Infrastructure.Repositories;
using WebConfigurationControl.Notifications.Hubs;
using WebConfigurationControl.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddControllers();
builder.Services.AddMediatR(AssemblyConfiguration.ExecutingAssembly, WebConfigurationControl.Notifications.AssemblyConfiguration.ExecutingAssembly);
builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);

builder.Services.AddScoped<ISystemConfigurationRepository, SystemConfigurationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventSubscriptionRepository, EventSubscriptionRepository>();
builder.Services.AddScoped<IEventNotificationRepository, EventNotificationRepository>();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapHub<EntityHub>("/notifications");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
