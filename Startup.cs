using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret.Contexts;
using E_Ticaret.Entities;
using E_Ticaret.Interfaces;
using E_Ticaret.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace E_Ticaret
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


            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddAuthentication();

            services.AddScoped<ISepetRepository, SepetRepository>();
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
            services.AddSession();
            services.AddControllersWithViews();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var dbContext = serviceProvider.GetRequiredService<Context>();

            IdentityResult roleResult1;
            IdentityResult roleResult2;
            //Adding Admin Role
            var roleCheck1 = await RoleManager.RoleExistsAsync("Admin");
            var roleCheck2 = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck1)
            {
                //create the roles and seed them to the database
                roleResult1 = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!roleCheck2)
            {
                //create the roles and seed them to the database
                roleResult2 = await RoleManager.CreateAsync(new IdentityRole("User"));
            }
           

            if (!dbContext.Users.Any(u => u.UserName == "g171210052@sakarya.edu.tr"))
            {
                var adminUser = new AppUser
                {
                    UserName = "g171210052@sakarya.edu.tr",
                    Email = "mmervekardess@gmail.com",
                };
                var result = await UserManager.CreateAsync(adminUser, "123");
                await UserManager.AddToRoleAsync(adminUser, new IdentityRole("Admin").Name);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment 
            env,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IServiceProvider serviceProvider)
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

          

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();//ilgili kullanýcý giriþ yapmýþ mý yapmamýl mý
            app.UseAuthorization();//giriþ yapan kullanýcýnýn yetkisi ilgili þartlarý karþýlýyor mu(admin mi girmiþ member mi girmiþ) 

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area}/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
              

            });
            CreateUserRoles(serviceProvider).Wait();
        }

    }

}
