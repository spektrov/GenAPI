using GenApi.Domain.Interfaces;
using GenApi.DomainServices.Services;
using Microsoft.Build.Locator;
using Microsoft.Extensions.DependencyInjection;

namespace GenApi.DomainServices;
public static class Bootstrap
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Initialize MSBuild for in-memory project creation.
        MSBuildLocator.RegisterDefaults();

        services.AddScoped<ISolutionGenService, SolutionGenService>();
        services.AddScoped<IFileGenService, FileGenService>();

        return services;
    }
}
