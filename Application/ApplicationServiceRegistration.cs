﻿using Application.Services.Authors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Validation;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Abstraction;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Configurations;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Serilog.File;
using NArchitecture.Core.Localization.Resource.Yaml.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        FileLogConfiguration fileLogConfiguration
    )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });

        //services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules)); // problem with this can not use BusinessRules
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddYamlResourceLocalization();

        services.AddSingleton<ILogger, SerilogFileLogger>(_ => new SerilogFileLogger(fileLogConfiguration));

        services.AddScoped<IAuthorService, AuthorManager>();

        return services;
    }

    //public static IServiceCollection AddSubClassesOfType(
    //    this IServiceCollection services,
    //    Assembly assembly,
    //    Type type,
    //    Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    //)
    //{
    //    var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
    //    foreach (Type? item in types)
    //        if (addWithLifeCycle == null)
    //            services.AddScoped(item);
    //        else
    //            addWithLifeCycle(services, type);
    //    return services;
    //}
}
