using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace WebApi.Extensions
{
    public static class ControllersExtension
    {
        public static void AddControllerExtension(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        }
    }
}