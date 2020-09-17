using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        public ActionResult ListEmployee()
        {
            
            return View();
        }

        public ActionResult AddEmployee()
        {
            String dummyPrefix = string.Empty;
            ViewBag.CompanyName = new SelectList(EmployeeDataAccess.SearchStringCompanyName(dummyPrefix), "mCompanyId", "companyName");
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataEmployee(EmployeeViewModel paramAddEmployee)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddEmployee.isDelete = false;

                //update data manual createby and createdate
                paramAddEmployee.createBy = "Anastasia";
                paramAddEmployee.createDate = DateTime.Now;

                string latestCode = EmployeeDataAccess.CreateEmployee(paramAddEmployee);

                if (latestCode != null)
                {
                    return Json(new { success = true, latestCode, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ListCompany(CompanyViewModel paramSearch)
        {
            List<CompanyViewModel> listSearchCompany = CompanyDataAccess.ListSearchCompany(paramSearch);

            return View(listSearchCompany);
        }


        public ActionResult SearchCompany(EmployeeViewModel paramSearch)
        {
            List<EmployeeViewModel> listSearchCompany = EmployeeDataAccess.GetListEmployee(paramSearch);

            return View(listSearchCompany);
        }


        [HttpPost]
        public JsonResult AutoCompleteCompanyName(string prefix)
        {
            var item = EmployeeDataAccess.SearchStringCompanyName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteEmployeeName(string prefix)
        {
            var item = EmployeeDataAccess.SearchStringEmployeeName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AutoCompleteEmployeeNumber(string prefix)
        {
            var item = EmployeeDataAccess.SearchStringEmployeeNumber(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}