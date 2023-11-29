using Microsoft.OpenApi.Models;

namespace CryptoLocalBack.Extensions
{
    internal static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CryptoLocalBackClient"
                });
                options.AddServer(new OpenApiServer { Url = "https://localhost:7098" });

                options.UseInlineDefinitionsForEnums();
                options.UseAllOfToExtendReferenceSchemas();

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
            });
        }
    }
}
