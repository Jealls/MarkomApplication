using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult ListCompany()
        {
            return View();
        }

        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataCompany(CompanyViewModel paramAddCompany)
        {   
            //is delete default value
            paramAddCompany.isDelete = false;
            
            //update data manual createby and createdate
            paramAddCompany.createBy = "Anastasia";
            paramAddCompany.createDate = DateTime.Now;

            if (CompanyDataAccess.CreateCompany(paramAddCompany)) {
                
                return Json(new { success = true, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}