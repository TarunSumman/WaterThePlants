using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WaterThePlants.Hubs
{
    public class NotifyAllUsersHub : Hub
    {
        public void OnHoldPlants(int[] plantID)
        {
            Clients.All.OnHoldPlants(plantID);
        }
        public void OnUnHoldPlants(int[] plantID)
        {
            Clients.All.OnUnHoldPlants(plantID);
        }

        public void OnCompleteWateringPlants(int[] plantID)
        {
            Clients.All.OnCompleteWateringPlants(plantID);
        }
        public void OnReminder(int plantID)
        {
            Clients.All.OnReminder(plantID);
        }
        public void OnFreeToWater(int plantID)
        {
            Clients.All.OnFreeToWater(plantID);
        }
    }
}