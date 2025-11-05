using Microsoft.Extensions.DependencyInjection;

namespace Linkify.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}