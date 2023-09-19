using Application.Contracts.Managers;
using Core.Handlers;
using Core.Implementations.Handlers;
using Core.Implementations.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Application.Managers;
using System.Reflection;

namespace Configurations
{
    public static class AppConfigurations
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("Application"));

            RegisterServices(services);
            RegisterHandlers(services);
            RegisterManagers(services);
        }

        private static void RegisterManagers(IServiceCollection services)
        {
            services.AddScoped<IProductionPlanManager, ProductionPlanManager>();
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            services.AddScoped<IPowerPlantHandler, PowerPlantHandlers>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));
        }
    }
}