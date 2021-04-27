using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaxiparkWeb.Models;

namespace TaxiparkWeb.Controllers
{
    public class DriverController : Controller
    {
        public ActionResult DriverIndex()
        {
            List<DriverModel> driverModel = TaxiDbContext.ReturnListAll<DriverModel>("SELECT * FROM public.\"Driver\"").ToList();
            
            foreach(DriverModel m in driverModel)
                m.AutoModels = TaxiDbContext.ReturnListAll<AutoModel>("Select * from public.\"Auto\" where \"AutoId\" = " + m.AutoId).ToList();

            return Json(new { data = driverModel }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAutoList()
        {
            var cars = TaxiDbContext.ReturnListAll<AutoModel>("Select * from public.\"Auto\"").ToList();

            return Json(cars, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DriverDataEdit(int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    DriverModel driverModel = new DriverModel();
                    
                    var autoModels = TaxiDbContext.ReturnListAll<AutoModel>
                        ("Select * from public.\"Auto\"").ToList();

                    var car = autoModels
                        .Select(x => new
                        {
                            AutoId = x.AutoId,
                            Brand = x.Brand + " " + x.Model
                        });

                    ViewBag.AutoID = new SelectList(car, "AutoId", "Brand");

                    return View(driverModel);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                DriverModel driverModel = TaxiDbContext.ReturnListAll<DriverModel>
                        ("Select * from public.\"Driver\" Where \"Id\" = " + id).FirstOrDefault<DriverModel>();
                driverModel.AutoModels = TaxiDbContext.ReturnListAll<AutoModel>
                        ("Select * from public.\"Auto\"").ToList();
                return View(driverModel);
            }
                
        }

        [HttpPost]
        public ActionResult DriverDataEdit(DriverModel driverModel)
        {
            if (driverModel.Id == 0)
            {
                TaxiDbContext.ExecuteWithoutReturn(
                    "INSERT INTO public.\"Driver\"(\"Name\", \"AutoId\") " +
                    "VALUES('" + driverModel.Name + "','" + driverModel.AutoId + "')");
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TaxiDbContext.ExecuteWithoutReturn(
                    "UPDATE public.\"Driver\" SET \"Name\" = '" + driverModel.Name +
                    "', \"AutoId\" = '" + driverModel.AutoId +
                    "' WHERE \"Id\" = " + driverModel.Id + ";");
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DriverDelete(int id)
        {
            TaxiDbContext.ExecuteWithoutReturn(
             "DELETE FROM public.\"Driver\" WHERE \"Id\" = " + id + ";");
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}