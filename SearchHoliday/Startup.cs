using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data;
using MySql.Data.MySqlClient;
using SearchHoliday.Implementations;
using SearchHoliday.Interfaces;
using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(
                "server=localhost;uid=root;password=root;persistsecurityinfo=True;database=searchholiday;sslMode=none",
                new MySqlServerVersion(new Version(8, 0, 11))
            ));

            services.AddTransient<IAllRecomendedHouses, AllRecomendedHouses>();
            services.AddTransient<IAllHouses, AllHouses>();
            services.AddTransient<IHouseDescription, HousesDescription>();
            services.AddTransient<IHousesQuestions, HousesQuestions>();
            //services.AddTransient<IUser, User>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseExceptionHandler("/Error");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Account}/{action=Login}");
            });
        }
    }
}
