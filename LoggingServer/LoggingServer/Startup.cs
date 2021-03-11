using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LoggingServer
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
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddLogging(loggingBuilder =>
            {
                var loggingSection = Configuration.GetSection("Logging");
                loggingBuilder.AddFile("EventLogs\\app_{0:yyyy}-{0:MM}-{0:dd}.log", fileLoggerOpts =>
                {
                    fileLoggerOpts.Append = true;
                    fileLoggerOpts.FileSizeLimitBytes = 1048576;
                    fileLoggerOpts.MaxRollingFiles = 5;
                    fileLoggerOpts.FormatLogFileName = fName =>
                    {
                        return String.Format(fName, DateTime.UtcNow);
                    };
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}