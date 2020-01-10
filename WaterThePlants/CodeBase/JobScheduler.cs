using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaterThePlants.CodeBase
{
    public class JobScheduler
    {
        public static void T_Elapsed()
        {
            dbContectionDataContext context = new dbContectionDataContext();
            var plants = context.Plants.ToList();

            foreach (var plant in plants)
            {
                if ((plant.LastCompleted < DateTime.Now.AddHours(-6)) && plant.Status == 'W')
                {
                    RealTimeCalls.NotifyOnReminder(plant.PlantID);
                    Common.StatusUpdate(plant.PlantID, 'E');
                }
                else if ((plant.Reset < DateTime.Now.AddSeconds(-10)) && plant.Status == 'H')
                {
                    Common.StatusUpdate(plant.PlantID, 'W');
                }
                else if ((plant.Reset < DateTime.Now.AddSeconds(-30)) && plant.Status == 'R')
                {
                    Common.StatusUpdate(plant.PlantID, 'W');
                }
            }
        }
    }
}