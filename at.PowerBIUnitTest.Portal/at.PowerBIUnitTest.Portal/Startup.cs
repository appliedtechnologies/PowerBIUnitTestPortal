using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using at.PowerBIUnitTest.Portal.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;

namespace at.PowerBIUnitTest.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users");
            builder.EntitySet<Tenant>("Tenants");
            builder.EntitySet<UnitTest>("UnitTests");
            builder.EntitySet<UserStory>("UserStories");
            builder.EntitySet<Workspace>("Workspaces");
            builder.EntitySet<TabularModel>("TabularModels");
            builder.EntitySet<TestRun>("TestRuns");
            builder.EntitySet<TestRun>("ResultTypes");
            builder.EntitySet<TestRunCollection>("TestRunCollections");
            builder.EntityType<User>().Collection.Action("Login");
            builder.EntityType<UnitTest>().Collection.Action("Execute");
            builder.EntityType<Workspace>().Collection.Action("Pull");

            var copyUserStory = builder.EntityType<UserStory>().Action("Copy");
            copyUserStory.Parameter<int>("targetTabularModelId");

            var copy = builder.EntityType<UserStory>().Action("Copy2");
            copy.Parameter<int>("targetTabularModelId1");
            copy.Parameter<int>("targetWorkspaceId1");
            copy.Parameter<int>("userStoryId1");

            return builder.GetEdmModel();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAd")
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddDownstreamWebApi("GraphApi", Configuration.GetSection("DownstreamApis:GraphApi"))
                .AddDownstreamWebApi("AzureManagementApi", Configuration.GetSection("DownstreamApis:AzureManagementApi"))
                .AddInMemoryTokenCaches();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddDbContext<PortalDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("AzureDbConnection")));

            services.AddControllers(options => { options.Filters.Add<UnhandledExceptionFilterAttribute>(); }).AddOData(options => options.AddRouteComponents("odata", GetEdmModel()).Select().Count().Filter().OrderBy().Expand().SetMaxTop(100));

            services.AddSwaggerGen();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<PowerBiService>();
            services.AddScoped<WorkspaceService>();
            services.AddScoped<TabularModelService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PortalDbContext dbContext)
        {
            var cultureInfo = new CultureInfo("de-DE");
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = new List<CultureInfo>
                {
                    cultureInfo,
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    cultureInfo,
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                dbContext.Database.MigrateAsync();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    var currentUri = "http://localhost:4222/";
                    var envUri = System.Environment.GetEnvironmentVariable("ASPNETCORE_ANGULAR_URL");
                    if (!string.IsNullOrWhiteSpace(envUri))
                    {
                        currentUri = envUri;
                    }
                    spa.UseProxyToSpaDevelopmentServer(currentUri);
                }
            });
        }
    }
}
