using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Simple Payment",
                    Description = "Challenge created for testing payment methods."
                });
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Payment - V1");
                c.RoutePrefix = string.Empty;
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });
        }
    }
}