using BlogZ.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.DependencyInjection;
using Persistence.Repositories;
using Persistence.Repositories.Abstractions;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<ApplicationDbContext>());

        services.AddDbMigrationApplier(buildServices =>
            buildServices.GetRequiredService<ApplicationDbContext>());

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                     .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IAuthorRepository, AuthorRepository>();

        return services;
    }
}

