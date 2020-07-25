using System;
using HackTecBanTimeSete.Areas.Identity.Data;
using HackTecBanTimeSete.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HackTecBanTimeSete.Areas.Identity.IdentityHostingStartup))]
namespace HackTecBanTimeSete.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<HackTecBanTimeSeteContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("HackTecBanTimeSeteContextConnection")));

                services.AddDefaultIdentity<HackTecBanTimeSeteUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<HackTecBanTimeSeteContext>();
            });
        }
    }
}