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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IAllRecomendedHouses, AllRecomendedHouses>();
            services.AddTransient<IAllHouses, AllHouses>();
            services.AddTransient<IHouseDescription, HousesDescription>();
            services.AddTransient<IHousesQuestions, HousesQuestions>();
            //services.AddTransient<IUser, User>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
