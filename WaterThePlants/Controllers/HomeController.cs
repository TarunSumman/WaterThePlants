using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaterThePlants.CodeBase;
using WaterThePlants.Models;

namespace WaterThePlants.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            dbContectionDataContext dbContext = new dbContectionDataContext();
            List<PlantModel> list = new List<PlantModel>();

            var plants = dbContext.Plants.ToList();

            foreach (var item in plants)
            {
                PlantModel model = new PlantModel();
                model.PlantID = item.PlantID;
                model.Status = item.Status.ToString();
                //model.LastCompleted = item.LastCompleted;

                list.Add(model);
            }
            return View(list);
        }
        [HttpGet]
        public JsonResult GetPlantList()
        {
            dbContectionDataContext dbContext = new dbContectionDataContext();
            List<PlantModel> list = new List<PlantModel>();

            var plants = dbContext.Plants.ToList();

            foreach (var item in plants)
            {
                PlantModel model = new PlantModel();
                model.PlantID = item.PlantID;
                model.Status = item.Status.ToString();
                model.PlantName = item.PlantName.ToString();
                model.LastWateredTime = Common.GetPrettyDate(Convert.ToDateTime(item.LastCompleted));
                DateTime Expired = Convert.ToDateTime(item.Reset);
                if (Expired > DateTime.Now) { 
                    var diffInSeconds = (Expired - DateTime.Now).TotalSeconds;
                    model.TimeLeft = Convert.ToInt32(diffInSeconds);
                }
                list.Add(model);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult HoldWatering(int[] plantId)
        {
            dbContectionDataContext context = new dbContectionDataContext();
            foreach(var id in plantId)
            {
                var plant = context.Plants.Single(x => x.PlantID == id);
                plant.Status = 'H';
                DateTime Reset = DateTime.Now.AddSeconds(10);
                plant.Reset = Reset;
                context.SubmitChanges();
                RealTimeCalls.NotifyOnHoldPlants(plantId);
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UnHoldWatering(int[] plantId)
        {
            dbContectionDataContext context = new dbContectionDataContext();
            foreach (var id in plantId)
            {
                var plant = context.Plants.Single(x => x.PlantID == id);
                plant.Status = 'W';
                context.SubmitChanges();
                RealTimeCalls.NotifyOnUnHoldPlants(plantId);
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult HoldReWateringPlants(int[] plantId)
        {
            dbContectionDataContext context = new dbContectionDataContext();
            foreach (var id in plantId)
            {
                var plant = context.Plants.Single(x => x.PlantID == id);
                plant.Status = 'R';
                plant.LastCompleted = DateTime.Now;
                DateTime Reset = DateTime.Now.AddSeconds(30);
                plant.Reset = Reset;
                context.SubmitChanges();
            }
            RealTimeCalls.NotifyOnCompletePlants(plantId);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CompleteWateringPlants(int plantId)
        {
            dbContectionDataContext context = new dbContectionDataContext();

            var plant = context.Plants.Single(x => x.PlantID == plantId);
            plant.Status = 'W';
            context.SubmitChanges();

            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}