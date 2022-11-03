using CalculatorAPI.Core.Interfaces.Services;
using CalculatorAPI.Core.Interfaces.Settings;
using CalculatorAPI.Core.Services;
using CalculatorAPI.Infrastructure.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

namespace CalculatorAPI
{
    public class Startup
    {
        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDbSettings>(
                new DbSettings { DefaultConnection = Configuration.GetSection("ConnectionStrings")["DefaultConnection"] });

            // services.AddScoped<IRepository, Repository>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            services.AddSingleton<IJournalService, JournalService>();

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CalculatorAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(options => options.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/plain";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    Console.WriteLine(exceptionHandlerPathFeature?.Error);
                    await context.Response.WriteAsync("An unexpected error condition was triggered which made impossible" +
                        " to fulfill the request. Please try again or contact support.");
                }));
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CalculatorAPI");
                c.DisplayRequestDuration();
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
