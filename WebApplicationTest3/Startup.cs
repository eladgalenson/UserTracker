using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsTracker.Data;
using FriendsTracker.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationTest3.Data;

namespace WebApplicationTest3
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _hostingEnvironment = env;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(cfg => {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = true;
                cfg.Password.RequireUppercase = true;
                cfg.Password.RequiredLength = 8;
            }
                ).AddEntityFrameworkStores<UserTrackingContext>();


            services.AddScoped<SeedGenerator>();

            services.AddDbContext<UserTrackingContext>(cfg =>
            {
                cfg.UseSqlServer(_configuration.GetConnectionString("FriendsTrackerConnectionString"));
            });

            services.AddMvc();

            services.AddTransient<IUserTrackingRepository, UserTrackingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seed = scope.ServiceProvider.GetService<SeedGenerator>();
                    seed.Seed().Wait();
                }
            }

            

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication(); // should be before UseMvc

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=UserTracking}/{action=All}/{id?}");
            });
        }
    }
}
