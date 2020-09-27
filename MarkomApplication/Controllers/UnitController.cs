using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class UnitController : Controller
    {
        // GET: Unit
        public ActionResult ListUnit()
        {
            return View();
        }

        public ActionResult AddUnit()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataUnit(UnitViewModel paramAddUnit)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddUnit.isDelete = false;
                //update data manual createby and createdate
                paramAddUnit.createBy = "Administator";
                paramAddUnit.createDate = DateTime.Now;


                int? nameV = UnitDataAccess.NameValidation(paramAddUnit.name);

                if (nameV == 0)
                {
                    string latestCode = UnitDataAccess.CreateUnit(paramAddUnit);

                    if (latestCode != "")
                    {
                        return Json(new { success = true, latestCode, message = UnitDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = UnitDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Unit name " + paramAddUnit.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult Index(UnitViewModel paramSearch)
        {
            List<UnitViewModel> listSearchUnit = UnitDataAccess.GetListUnit(paramSearch);

            return PartialView(listSearchUnit);
        }


        //EDIT ROLE
        public ActionResult EditUnit(int paramId)
        {
            return PartialView(UnitDataAccess.GetDetailUnitById(paramId));
        }

        [HttpPost]
        public ActionResult EditDataUnit(UnitViewModel paramEditUnit)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditUnit.updateBy = "Administator";
                paramEditUnit.updateDate = DateTime.Now;

                int? nameV = UnitDataAccess.NameValidation(paramEditUnit.name);

                if (nameV <= 1)
                {
                    if (UnitDataAccess.UpdateUnit(paramEditUnit))
                    {
                        return Json(new { success = true, message = UnitDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = UnitDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Unit name " + paramEditUnit.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        //VIEW ROLE
        public ActionResult ViewUnit(int paramId)
        {
            return PartialView(UnitDataAccess.GetDetailUnitById(paramId));
        }



        //DELETE EMPLOYEE
        [HttpPost]
        public JsonResult DeleteDataUnit(int paramId)
        {
            string latestCode = UnitDataAccess.DeleteUnit(paramId);

            if (latestCode != "")
            {
                return Json(new { success = true, latestCode, message = UnitDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = UnitDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}