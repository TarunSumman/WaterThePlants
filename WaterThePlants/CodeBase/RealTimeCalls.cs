using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaterThePlants.CodeBase
{
    public class RealTimeCalls
    {
        public static void NotifyOnHoldPlants(int[] plantId)
        {
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.NotifyAllUsersHub>();
            hubContext.Clients.All.OnHoldPlants(plantId);
        }
        public static void NotifyOnCompletePlants(int[] plantId)
        {
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.NotifyAllUsersHub>();
            hubContext.Clients.All.OnCompleteWateringPlants(plantId);
        }
        public static void NotifyOnUnHoldPlants(int[] plantId)
        {
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.NotifyAllUsersHub>();
            hubContext.Clients.All.OnUnHoldPlants(plantId);
        }
        public static void NotifyOnReminder(int plantId)
        {
            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.NotifyAllUsersHub>();
            hubContext.Clients.All.OnReminder(plantId);
        }
    }
}