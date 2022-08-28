using Hostel.Shared.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hostel.Catalogue.Infrastructure.Dal.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.ConfigureOptions<DatabaseOptionsSetup>();

            services.AddDbContext<CatalogueContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                dbContextOptionsBuilder.UseSqlServer(databaseOptions.DefaultConnection, sqlServerAction =>
                {
                    sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);

                    sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);

                });

                dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

                dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });

            return services;
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder applicationBuilder,
            IConfiguration configuration)
        {
            if (applicationBuilder is null)
                throw new ArgumentNullException(nameof(applicationBuilder));

            if (configuration is null)
                throw new ArgumentNullException(nameof(applicationBuilder));

            if (bool.TryParse(configuration["Db:Migration:Enable"], out var migrateDatabase) && migrateDatabase)
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<CatalogueContext>();
                    context.Database.Migrate();
                }
            }

            return applicationBuilder;
        }
    }
}
