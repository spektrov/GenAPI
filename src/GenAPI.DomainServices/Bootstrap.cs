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
        AddCommands(services);
        services.AddAutoMapper(typeof(Bootstrap).Assembly);
        services.AddScoped<ISolutionGenService, SolutionGenService>();
        services.AddScoped<IFileGenService, FileGenService>();

        return services;
    }

    private static void AddCommands(IServiceCollection services)
    {
        var commandImplementationTypes = typeof(Bootstrap).Assembly.GetTypes()
            .Where(type => typeof(IGenCommand).IsAssignableFrom(type) && !type.IsInterface);

        foreach (var commandImplementationType in commandImplementationTypes)
        {
            services.AddScoped(typeof(IGenCommand), commandImplementationType);
        }
    }
}
