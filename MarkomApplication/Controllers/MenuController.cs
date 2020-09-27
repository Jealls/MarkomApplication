using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult ListMenu()
        { 
            return View();
        }

        public ActionResult AddMenu()
        {
            //String dummyPrefix = string.Empty;
            //ViewBag.MenuParentName = new SelectList(MenuDataAccess.SearchMenuStringParent(dummyPrefix), "parentId", "parentName");
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataMenu(MenuViewModel paramAddMenu)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddMenu.isDelete = false;
                //update data manual createby and createdate
                paramAddMenu.createBy = "Zack";
                paramAddMenu.createDate = DateTime.Now;


                int? numberV = MenuDataAccess.NameValidation(paramAddMenu.code);

                if (numberV == 0)
                {
                    string latestCode = MenuDataAccess.CreateMenu(paramAddMenu);

                    if (latestCode != "")
                    {
                        return Json(new { success = true, latestCode, message = MenuDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = MenuDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Menu name " + paramAddMenu.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }



        public ActionResult Index(MenuViewModel paramSearch)
        {
            List<MenuViewModel> listSearchRole = MenuDataAccess.GetListMenu(paramSearch);

            return PartialView(listSearchRole);
        }



        //EDIT Menu
        public ActionResult EditMenu(int paramId)
        {
            //String dummyPrefix = string.Empty;
            //ViewBag.MenuParentName = new SelectList(MenuDataAccess.SearchMenuStringParent(dummyPrefix), "parentId", "parentName");

            return PartialView(MenuDataAccess.GetDetailMenuById(paramId));
        }


        [HttpPost]
        public ActionResult EditDataMenu(MenuViewModel paramEditMenu)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditMenu.updateBy = "Tian";
                paramEditMenu.updateDate = DateTime.Now;


                int? numberV = MenuDataAccess.NameValidation(paramEditMenu.code);

                if (numberV <= 1)
                {
                    if (MenuDataAccess.UpdateMenu(paramEditMenu))
                    {
                        return Json(new { success = true, message = MenuDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = MenuDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Menu number " + paramEditMenu.code + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        //VIEW MENU
        public ActionResult ViewMenu(int paramId)
        {
            return PartialView(MenuDataAccess.GetDetailMenuById(paramId));
        }



        //DELETE EMPLOYEE
        [HttpPost]
        public JsonResult DeleteDataMenu(int paramId)
        {
            string latestCode = MenuDataAccess.DeleteMenu(paramId);

            if (latestCode != "")
            {
                return Json(new { success = true, latestCode, message = MenuDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = MenuDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult AutoCompleteMenuCode(string prefix)
        {

            var item = MenuDataAccess.SearchMenuStringCode(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteMenuName(string prefix)
        {
            var item = MenuDataAccess.SearchMenuStringName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoCompleteMenuParent(string prefix)
        {

            var item = MenuDataAccess.SearchMenuStringParent(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}