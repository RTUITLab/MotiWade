using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Database.SQLite
{
    public static class DatabaseExtensions
    {
        public static void RegisterSqliteDbContext(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(DatabaseExtensions).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<LearnDbContext>(options =>
                options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
