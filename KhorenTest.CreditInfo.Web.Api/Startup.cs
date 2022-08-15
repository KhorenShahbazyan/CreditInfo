using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KhorenTest.CreditInfo.Business.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace KhorenTest.CreditInfo.Web.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                    .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {               
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                   builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvcCore();
            services.AddDirectoryBrowser();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/event-stream" });
            });

            services.AddOptions();
            services.AddDbContext(Configuration);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KhorenTest.CreditInfo API", Version = "v1" });                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KhorenTest.CreditInfo API V1");
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("KhorenTest.CreditInfo API");
                });
                // endpoints.MapControllers();
            });
            app.UseMvc();
        }
    }
}
