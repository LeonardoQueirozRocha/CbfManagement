using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Interfaces.Services;
using Cbf.Business.Notificacoes;
using Cbf.Business.Services;
using Cbf.Data.Context;
using Cbf.Data.Repository;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cbf.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDepencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<ITimeRepository, TimeRepository>();
            services.AddScoped<IJogadorRepository, JogadorRepository>();
            services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();
            services.AddScoped<ITorneioRepository, TorneioRepository>();
            services.AddScoped<IPartidaRepository, PartidaRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IJogadorService, JogadorService>();
            services.AddScoped<ITorneioService, TorneioService>();
            services.AddScoped<IPartidaService, PartidaService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
