using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class SouvenirController : Controller
    {
        // GET: Souvenir
        public ActionResult ListSouvenir()
        {
            return View();
        }

        public ActionResult AddSouvenir()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataSouvenir(SouvenirViewModel paramAddSou)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddSou.isDelete = false;
                //update data manual createby and createdate
                paramAddSou.createBy = "Administator";
                paramAddSou.createDate = DateTime.Now;


                int? nameV = SouvenirDataAccess.NameValidation(paramAddSou.name);

                if (nameV == 0)
                {
                    string latestCode = SouvenirDataAccess.CreateSouvenir(paramAddSou);

                    if (latestCode != "")
                    {
                        return Json(new { success = true, latestCode, message = SouvenirDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = SouvenirDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Souvenir name " + paramAddSou.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult Index(SouvenirViewModel paramSearch)
        {

            List<SouvenirViewModel> listSearchSou = SouvenirDataAccess.GetListSouvenir(paramSearch);

            return PartialView(listSearchSou);
        }


        //EDIT Souvenir
        public ActionResult EditSouvenir(int paramId)
        {
            return PartialView(SouvenirDataAccess.GetDetailSouvenirById(paramId));
        }

        [HttpPost]
        public ActionResult EditDataSouvenir(SouvenirViewModel paramEditSou)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditSou.updateBy = "Administator";
                paramEditSou.updateDate = DateTime.Now;

                int? nameV = SouvenirDataAccess.NameValidation(paramEditSou.name);

                if (nameV <= 1)
                {
                    if (SouvenirDataAccess.UpdateSouvenir(paramEditSou))
                    {
                        return Json(new { success = true, message = SouvenirDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = SouvenirDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Sou name " + paramEditSou.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        //VIEW Souvenir
        public ActionResult ViewSouvenir(int paramId)
        {
            return PartialView(SouvenirDataAccess.GetDetailSouvenirById(paramId));
        }



        //DELETE 
        [HttpPost]
        public JsonResult DeleteDataSouvenir(int paramId)
        {
            string latestCode = SouvenirDataAccess.DeleteSouvenir(paramId);

            if (latestCode != "")
            {
                return Json(new { success = true, latestCode, message = SouvenirDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = SouvenirDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //AUTO COMPLETE SECTION
        [HttpPost]
        public JsonResult AutoCompleteUnitName(string prefix)
        {
            var item = SouvenirDataAccess.SearchUnitName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteSouvenirName(string prefix)
        {
            var item = SouvenirDataAccess.SearchSouvenirName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AutoCompleteSouvenirCode(string prefix)
        {
            var item = SouvenirDataAccess.SearchSouvenirCode(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}