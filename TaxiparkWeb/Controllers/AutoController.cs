using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaxiparkWeb.Models;

namespace TaxiparkWeb.Controllers
{
    public class AutoController : Controller
    {
        public ActionResult AutoIndex()
        {
            //return View(TaxiDbContext.ReturnListAll<AutoModel>("Select * from public.\"Auto\"").ToList());
            List<AutoModel> autoModel = TaxiDbContext.ReturnListAll<AutoModel>("Select * from public.\"Auto\"").ToList();
            return Json(new { data = autoModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AutoDataEdit(int id = 0)
        {
            if (id == 0) return View();
            else
                return View(TaxiDbContext.ReturnListAll<AutoModel>("Select * from public.\"Auto\" Where \"AutoId\" = " + id)
                    .FirstOrDefault<AutoModel>());
        }

        [HttpPost]
        public ActionResult AutoDataEdit(AutoModel autoModel)
        {
            if (autoModel.AutoId == 0)
            {
                TaxiDbContext.ExecuteWithoutReturn(
                    "INSERT INTO public.\"Auto\"(\"Brand\", \"Model\") " +
                    "VALUES('" + autoModel.Brand + "','" + autoModel.Model + "')");
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }     
            else
            {
                TaxiDbContext.ExecuteWithoutReturn(
                    "UPDATE public.\"Auto\" SET \"Brand\" = '" + autoModel.Brand +
                    "', \"Model\" = '" + autoModel.Model +
                    "' WHERE \"AutoId\" = " + autoModel.AutoId + ";");
                return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
            }
                
            //return RedirectToAction("AutoIndex");
        }

        public ActionResult AutoDataDelete(int id)
        {
            TaxiDbContext.ExecuteWithoutReturn(
                "DELETE FROM public.\"Auto\" WHERE \"AutoId\" = " + id + ";");
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}