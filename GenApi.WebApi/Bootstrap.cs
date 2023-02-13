using GenApi.WebApi.Filters;

namespace GenApi.WebApi;

public static class Bootstrap
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Bootstrap).Assembly);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddMvc(options =>
        {
            options.Filters.Add<AddFileHeaderFilter>();
        });

        return services;
    }
}
