using FluentValidation;
using Full.Stack.Task.Application.Behaviors;
using Full.Stack.Task.Application.Contracts.Services;
using Full.Stack.Task.Application.Events.AuditLogs;
using Full.Stack.Task.Application.MappingConfig;
using Full.Stack.Task.Application.Services;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggerPipeLineBehavior<,>));
            });

            services.AddScoped<IJwtService, JwtService>();
            var configMapper = TypeAdapterConfig.GlobalSettings;
            configMapper.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(configMapper);
            services.AddScoped<IMapper, ServiceMapper>();

            MapsterConfig.Configure();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

            return services;
        }
    }
}
