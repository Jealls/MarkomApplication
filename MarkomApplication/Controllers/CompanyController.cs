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


        public ActionResult Index(CompanyViewModel paramSearch)
        {
            List<CompanyViewModel> listSearchCompany = CompanyDataAccess.ListSearchCompany(paramSearch);

            return PartialView(listSearchCompany);
        }


        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataCompany(CompanyViewModel paramAddCompany)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddCompany.isDelete = false;
            
                //update data manual createby and createdate
                paramAddCompany.createBy = "Anastasia";
                paramAddCompany.createDate = DateTime.Now;

                int? nameV = CompanyDataAccess.NameValidation(paramAddCompany.name);

                if (nameV == 0)
                {
                    string latestCode = CompanyDataAccess.CreateCompany(paramAddCompany);

                    if (latestCode != "")
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
                    return Json(new { success = false, message = "Company name "+paramAddCompany.name+" is exist !" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

                //return PartialView("Add", paramAddCompany);
            }
        }

        //GET detail company
        public ActionResult EditCompany(int paramId)
        {
            return PartialView(CompanyDataAccess.GetDetailCompanyById(paramId));
        }

        [HttpPost]
        public ActionResult EditDataCompany(CompanyViewModel paramEditCompany)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditCompany.updateBy = "Tian";
                paramEditCompany.updateDate = DateTime.Now;
                int? nameV = CompanyDataAccess.NameValidation(paramEditCompany.name);
                if (nameV <= 1)
                {
                    if (CompanyDataAccess.UpdateCompany(paramEditCompany))
                    {
                        return Json(new { success = true, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Company name " + paramEditCompany.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }

        //GET View Deatil company
        public ActionResult ViewCompany(int paramId)
        {
            return PartialView(CompanyDataAccess.GetDetailCompanyById(paramId));
        }

        [HttpPost]
        public JsonResult DeleteDataCompany(int paramId)
        {
            
                string latestCode = CompanyDataAccess.DeleteCompany(paramId);

                if (latestCode != "")
                {
                    return Json(new { success = true, latestCode, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = CompanyDataAccess.Message }, JsonRequestBehavior.AllowGet);
                }
        }


        [HttpPost]
        public JsonResult AutoCompleteCompanyCode(string prefix)
        {
            
            var item = CompanyDataAccess.SearchStringCode(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteCompanyName(string prefix)
        {
            var item = CompanyDataAccess.SearchStringName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }


//        [HttpPost]
//        public ActionResult SearchCompany(CompanyListViewModel paramSearch)
//        {
//            List<CompanyListViewModel> listSearchCompany = CompanyDataAccess.ListSearchCompany(paramSearch);

//            return RedirectToAction("ListCompany", new
//{
//                listSearchCompany
//            });
//            //return Json(Url.Action("ListCompany", "Company"), listSearchCompany, JsonRequestBehavior.AllowGet);
//        }
    }
}