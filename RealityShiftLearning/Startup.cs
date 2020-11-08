using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using Database;
using Database.Postgres;
using Database.SQLite;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using RealityShiftLearning.Areas.Identity;
using RealityShiftLearning.Options;
using RealityShiftLearning.Services;
using RealityShiftLearning.Services.Configure;
using RealityShiftLearning.Services.Hosted;
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
            services.AddScoped<TimerService>();

            switch (Configuration.GetValue<DBType>("DbType"))
            {
                case DBType.SQLite:
                    Console.WriteLine("USING SQLITE");
                    services.RegisterSqliteDbContext(Configuration.GetConnectionString("SQLite"));
                    break;
                case DBType.Postgres:
                    Console.WriteLine("USING POSTGRES");
                    services.RegisterPostgresDbContext(Configuration.GetConnectionString("Postgres"));
                    break;
            }

            services.AddIdentity<MotiWadeUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<LearnDbContext>();
            services.AddRazorPages();
            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();


            var gitHubOptions = Configuration.GetSection(nameof(GitHubOptions)).Get<GitHubOptions>();
            services.AddAuthentication()
                    .AddOAuth("GitHub", "GitHub", options =>
                    {
                        options.ClientId = gitHubOptions.ClientId;
                        options.ClientSecret = gitHubOptions.Secret;
                        options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                        options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                        options.UserInformationEndpoint = "https://api.github.com/user";
                        options.SaveTokens = false;
                        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                        options.ClaimActions.MapJsonKey("urn:github:login", "login");
                        options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                        options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");
                        options.CallbackPath = new PathString("/github-oauth");
                        options.Scope.Add("read:user");
                        options.Scope.Add("read:email");
                        options.Events = new OAuthEvents
                        {
                            OnCreatingTicket = async context =>
                        {
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var json = JsonDocument.Parse(jsonString);
                            context.RunClaimActions(json.RootElement);
                        }
                        };
                    });

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(Configuration.GetValue<string>("FireBaseAuthLocation"))
            });
            services.AddScoped<PushNotifyService>();
            services.AddTransient<RandomTasksService>();

            services.AddHostedService<TomatoCheckPointPushUpdater>();

            services.AddAntDesign();
            services.AddWebAppConfigure()
                .AddTransientConfigure<AutoMigration>(0)
                .AddTransientConfigure<DemoUserGeneration>(1);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

    }
}
