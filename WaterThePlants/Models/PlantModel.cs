using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaterThePlants.Models
{
    public class PlantModel
    {
        public int PlantID { get; set; }
        public string PlantName { get; set; }
        public string Status { get; set; }
        public int TimeLeft { get; set; }
        public string LastWateredTime { get; set; }
    }
}