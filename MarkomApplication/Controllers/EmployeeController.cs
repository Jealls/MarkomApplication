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

        //table LIST EMPLOYEE
        public ActionResult Index(string code, string fullName, int? mCompanyId, DateTime? createDate2, string createBy)
        {
            List<EmployeeViewModel> listSearchEmp = EmployeeDataAccess.GetListEmployee(code, fullName, mCompanyId, createDate2, createBy);

            return PartialView(listSearchEmp);
        }

        //public ActionResult Index(EmployeeViewModel paramSearch)
        //{
        //    List<EmployeeViewModel> listSearchCompany = EmployeeDataAccess.GetListEmployee(paramSearch);

        //    return PartialView(listSearchCompany);
        //}


        //  ADD EMPLOYEE
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
                
                int? numberV = EmployeeDataAccess.NumberValidation(paramAddEmployee.code);
                
                if (numberV == 0)
                {
                    string latestCode = EmployeeDataAccess.CreateEmployee(paramAddEmployee);

                    if (latestCode != "")
                    {
                        return Json(new { success = true, latestCode, message = EmployeeDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = EmployeeDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Employee number " + paramAddEmployee.code + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }




        //EDIT EMPLOYEE
        public ActionResult EditEmployee(int paramId)
        {
            String dummyPrefix = string.Empty;
            ViewBag.CompanyName = new SelectList(EmployeeDataAccess.SearchStringCompanyName(dummyPrefix), "mCompanyId", "companyName");

            return PartialView(EmployeeDataAccess.GetDetailEmployeeById(paramId));
        }


        [HttpPost]
        public ActionResult EditDataEmployee(EmployeeViewModel paramEditEmp)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditEmp.updateBy = "Tian";
                paramEditEmp.updateDate = DateTime.Now;

                int? numberV = EmployeeDataAccess.NumberValidation(paramEditEmp.code);

                if (numberV <= 1)
                {
                    if (EmployeeDataAccess.UpdateEmployee(paramEditEmp))
                    {
                        return Json(new { success = true, message = EmployeeDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = EmployeeDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Employee number " + paramEditEmp.code + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }




        //VIEW EMPLOYEE
        public ActionResult ViewEmployee(int paramId)
        {
            return PartialView(EmployeeDataAccess.GetDetailEmployeeById(paramId));
        }

        //DELETE EMPLOYEE
        [HttpPost]
        public JsonResult DeleteDataEmployee(int paramId)
        {
            string latestCode = EmployeeDataAccess.DeleteEmployee(paramId);

            if (latestCode != "")
            {
                return Json(new { success = true, latestCode, message = EmployeeDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = EmployeeDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        //AUTO COMPLETE SECTION
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