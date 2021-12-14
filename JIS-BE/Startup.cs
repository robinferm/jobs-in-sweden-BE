using JIS_BE.Models;
using JIS_BE.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace JIS_BE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JIS_BE", Version = "v1" });
            });

            services.Configure<JISDatabaseSettings>(Configuration.GetSection("JISDatabase"));

            services.AddSingleton<JobListingsService>();

            // Probably not needed anymore after using reverse proxy
            services.AddCors(options => options.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin()
                                                                                                .AllowAnyMethod()
                                                                                                .AllowAnyHeader()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JIS_BE v1"));
            }

            // Test
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JIS_BE v1"));

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowEverything");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
