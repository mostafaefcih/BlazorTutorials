using Syncfusion.Blazor;
using AutoMapper;
using DevExpress.Blazor.Reporting;
using DevExpress.XtraReports.Web.Extensions;
using EmployeeManagement.Web.Helpers;
using EmployeeManagement.Web.MapperProfile;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web
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
            services.AddSyncfusionBlazor();
            services.AddDevExpressBlazorReporting();
            // Register the storage after the AddDevExpressBlazorReporting method call.
            services.AddScoped<ReportStorageWebExtension, ReportStorageWebExtension1>();
            services.AddAuthentication("Identity.Application").
                AddCookie();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44352/");
            });
            services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44352/");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense
                ("NDA0MTk3QDMxMzgyZTM0MmUzMGZDczZsajFJRGFuaW9CNkZtNU9WTWM3ZGtXTjlTKzNYMmx5YjFQdWswbWc9;NDA0MTk4QDMxMzgyZTM0MmUzMEtBM3ZlVDQvMCt3TksvYkk5Uyttbi9LdjZvSUhkT2RrVWd6MUVOV2dvL0E9;NDA0MTk5QDMxMzgyZTM0MmUzMEVlcGQxL1ZSbUpSbndSSjJERjkwUm1DK2V3SytkdHhqWC90dXpUckZjYUk9");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDevExpressBlazorReporting();
            app.UseHttpsRedirection();
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
