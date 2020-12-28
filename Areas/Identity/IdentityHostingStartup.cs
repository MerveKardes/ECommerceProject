using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using E_Ticaret.Contexts;
using E_Ticaret.Entities;

[assembly: HostingStartup(typeof(E_Ticaret.Areas.Identity.IdentityHostingStartup))]
namespace E_Ticaret.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Context>().AddDefaultIdentity<AppUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 1;
                })
                    .AddRoles<IdentityRole>().AddEntityFrameworkStores<Context>();
            });
        }
    }
}