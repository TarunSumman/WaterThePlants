using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Timers;
using WaterThePlants.CodeBase;

[assembly: OwinStartupAttribute(typeof(WaterThePlants.Startup))]
namespace WaterThePlants
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Timer t = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds); // Set the time (5 mins in this case)
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(JobScheduler.T_Elapsed);
            t.Start();
            app.MapSignalR();
        }
    }
}
