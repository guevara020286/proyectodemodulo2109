using System;
using CBS_CC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CBS_CC.Areas.Identity.IdentityHostingStartup))]
namespace CBS_CC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CBS_CCSeguridadContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CBS_CCContext")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<CBS_CCSeguridadContext>();
            });
        }
    }
}