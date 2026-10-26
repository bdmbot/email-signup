using EmailSignup.Application.Interfaces;
using EmailSignup.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSignup.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISubscriberService, SubscriberService>();
        return services;
    }
}
