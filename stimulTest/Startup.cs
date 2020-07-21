using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using stimulTest.Builder;
using stimulTest.Controllers;

namespace stimulTest
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
            services.AddControllersWithViews();

            services.AddDbContext<RTProSLContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RTProDatabase"), x => x.UseNetTopologySuite())
                    .EnableDetailedErrors()
            );

            services.AddScoped<IDataToolsService, DataToolsService>();
            services.AddScoped<IRepairReportRepository, RepairReportRepository>();
            services.AddScoped<IReportService<RepairReportDto>, RepairReportBuilder>();
            services.AddScoped<IEquipmentInspectionRepository, EquipmentInspectionRepository>();
            services.AddScoped<IReportService<EquipmentInspectionDto>, EquipmentInspectionReportBuilder>();

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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
