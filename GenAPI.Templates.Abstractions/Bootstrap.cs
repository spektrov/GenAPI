using GenApi.Templates.Common;
using Microsoft.Extensions.DependencyInjection;

namespace GenApi.Templates.Parser;

public static class Bootstrap
{
    public static IServiceCollection AddTemplateParser(this IServiceCollection services)
    {
        services.AddScoped<ITemplateParser, TemplateParser>();

        return services;
    }
}