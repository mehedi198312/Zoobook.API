using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Zoobook.Data;
using Zoobook.Service;
using Zoobook.Service.Interfaces;
using Zoobook.UnitOfWork;
using Zoobook.UnitOfWork.Interfaces;

namespace Zoobook.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<EmployeeRecordsDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DevDbConnectionString"));
            });
            
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerDocument();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowZooBookWebApp",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEmployeeUOW, EmployeeUOW>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            
            app.UseRouting();
            app.UseCors("AllowZooBookWebApp");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
