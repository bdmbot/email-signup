using EmailSignup.Application;
using EmailSignup.Application.DTOs;
using EmailSignup.Application.Interfaces;
using EmailSignup.Infrastructure;
using EmailSignup.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Data Source=email-signup.db";

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/regions", (ISubscriberService service) =>
    Results.Ok(service.GetRegions()));

app.MapPost("/api/subscribe", async (
    SubscribeRequest request,
    ISubscriberService service,
    CancellationToken cancellationToken) =>
{
    var result = await service.SubscribeAsync(request, cancellationToken);
    return result.Success
        ? Results.Ok(result)
        : Results.BadRequest(result);
});

app.MapFallbackToFile("index.html");

app.Run();
