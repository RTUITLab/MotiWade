using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Database.Postgres
{
    public static class DatabaseExtensions
    {
        public static void RegisterPostgresDbContext(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(DatabaseExtensions).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<LearnDbContext>(options =>
                options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
