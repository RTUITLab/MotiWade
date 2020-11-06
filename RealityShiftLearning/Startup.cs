using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Database;
using Database.Postgres;
using Database.SQLite;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealityShiftLearning.Data;
using RealityShiftLearning.Services;
using RealityShiftLearning.Services.Configure;
using RTUITLab.AspNetCore.Configure.Configure;

namespace RealityShiftLearning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            
            switch (Configuration.GetValue<DBType>("DbType"))
            {
                case DBType.SQLite:
                    services.RegisterSqliteDbContext(Configuration.GetConnectionString("SQLite"));
                    break;
                case DBType.Postgres:
                    services.RegisterPostgresDbContext(Configuration.GetConnectionString("Postgres"));
                    break;
            }

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(Configuration.GetValue<string>("FireBaseAuthLocation"))
            });
            services.AddTransient<PushNotifyService>();

            services.AddAntDesign();
            services.AddWebAppConfigure()
                .AddTransientConfigure<AutoMigration>(0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
