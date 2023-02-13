using GenApi.Domain.Interfaces;
using GenApi.DomainServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GenApi.DomainServices;
public static class Bootstrap
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ISolutionGenService, SolutionGenService>();

        return services;
    }
}
