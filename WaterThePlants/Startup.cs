using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using WaterThePlants.CodeBase;

[assembly: OwinStartupAttribute(typeof(WaterThePlants.Startup))]
namespace WaterThePlants
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
            GlobalConfiguration.Configuration.UseSqlServerStorage("testdbConnectionString");
            app.UseHangfireDashboard();
            Hangfire.RecurringJob.AddOrUpdate(() => JobScheduler.T_Elapsed(), Hangfire.Cron.Minutely);
            app.UseHangfireServer();
            app.MapSignalR();
        }
    }
}
