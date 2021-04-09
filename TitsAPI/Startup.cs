using System.IO;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();


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