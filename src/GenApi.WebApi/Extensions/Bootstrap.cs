namespace GenApi.WebApi.Extensions;

public static class Bootstrap
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Bootstrap).Assembly);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
