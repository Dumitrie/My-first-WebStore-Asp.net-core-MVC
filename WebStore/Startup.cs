using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.BusinessLogic;
using WebStore.DataAccessLayer;
using WebStore.Models;
using WebStore.Services;

namespace WebStore
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration["Data:WebStore:ConnectionString"]
                ));
           
            services.AddTransient<IProductRepository, MsSqlRepository>(); 
            
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(
                Configuration["Data:WebStore:ConnectionString"]
                ));

            services.AddIdentity<AppUserModel, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();

            services.AddScoped(ServiceCart.GEtCart);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<Cart>(sp => ServiceCart.GEtCart(sp)); �� �� �����
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStatusCodePages();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "CategorySimple",
                    pattern: "{category}",
                    defaults: new { Controller = "Product", action = "ProductsView" , pageNumber = 1 });

                endpoints.MapControllerRoute(
                    name: "CategoryPage",
                    pattern: "{category}/Products/Page{ pageNumber}",
                    defaults: new { Controller = "Product", action = "ProductsView"});


                endpoints.MapControllerRoute(
                    name: "paginator",
                    pattern: "Products/Page{pageNumber}",
                    defaults: new {Controller= "Product", action="ProductsView"});

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            DataManager.SeedData(app);
        }
    }
}
