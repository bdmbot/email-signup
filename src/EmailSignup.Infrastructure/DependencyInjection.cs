using EmailSignup.Application.Interfaces;
using EmailSignup.Infrastructure.Persistence;
using EmailSignup.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSignup.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        return services;
    }
}
