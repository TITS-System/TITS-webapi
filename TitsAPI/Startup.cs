using System.IO;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.AutoMapperProfiles;
using Services.Implementations;

namespace TitsAPI
{
    public class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string WWWRootPath { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Error =
                            (sender, args) => _logger.LogCritical(args.ErrorContext.Error.Message);
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                );

            // DbContext will take connection string from Environment or throw
            services.AddDbContext<TitsDbContext>();

            // Add Repositories
            services.AddScoped<ITokenSessionRepository, TokenSessionRepository>();
            services.AddScoped<IWorkerAccountRepository, WorkerAccountRepository>();
            services.AddScoped<IWorkerRoleRepository, WorkerRoleRepository>();
            services.AddScoped<IWorkerToRoleRepository, WorkerToRoleRepository>();

            // Add Services
            services.AddScoped<ITokenSessionService, TokenSessionService>();
            services.AddScoped<IWorkerAccountService, WorkerAccountService>();
            services.AddScoped<IWorkerRoleService, WorkerRoleService>();

            services.AddAutoMapper(cfg => cfg.AddProfile(new TitsAutomapperProfile()));

            services.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new OpenApiInfo() {Title = "TITS Swagger"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My First Swagger");
                c.RoutePrefix = "docs";
            });

            WWWRootPath = Path.GetFullPath("../TITS_front", env.ContentRootPath);

            app.UseHttpsRedirection();

            _logger = loggerFactory.CreateLogger<Startup>();

            _logger.LogInformation(WWWRootPath);

            env.WebRootFileProvider = new PhysicalFileProvider(WWWRootPath);

            app.UseDefaultFiles(); // Serve index.html for route "/"
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = env.WebRootFileProvider,
                ServeUnknownFileTypes = true
            });

            app.UseRouting();

            app.UseCors(builder => builder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "api_area",
                    areaName: "API",
                    pattern: "api/{controller}/{action}");

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}