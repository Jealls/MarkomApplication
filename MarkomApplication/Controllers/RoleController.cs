using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult ListRole()
        {
            return View();
        }

        public ActionResult AddRole()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataRole(RoleViewModel paramAddRole)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddRole.isDelete = false;
                //update data manual createby and createdate
                paramAddRole.createBy = "Anastasia";
                paramAddRole.createDate = DateTime.Now;


                bool nameV = RoleDataAccess.NameValidation(paramAddRole.name);

                if (!nameV)
                {
                    string latestCode = RoleDataAccess.CreateRole(paramAddRole);

                    if (latestCode != "")
                    {
                        return Json(new { success = true, latestCode, message = RoleDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = RoleDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Role name " + paramAddRole.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult Index(RoleViewModel paramSearch)
        {
            List<RoleViewModel> listSearchRole = RoleDataAccess.GetListRole(paramSearch);

            return PartialView(listSearchRole);
        }


        //EDIT ROLE
        public ActionResult EditRole(int paramId)
        {
            return PartialView(RoleDataAccess.GetDetailRoleById(paramId));
        }

        [HttpPost]
        public ActionResult EditDataRole(RoleViewModel paramEditRole)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditRole.updateBy = "Tian";
                paramEditRole.updateDate = DateTime.Now;
                
                bool nameV = RoleDataAccess.NameValidation(paramEditRole.name);

                if (!nameV)
                {
                    if (RoleDataAccess.UpdateRole(paramEditRole))
                    {
                        return Json(new { success = true, message = RoleDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = RoleDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Role name " + paramEditRole.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        //VIEW ROLE
        public ActionResult ViewRole(int paramId)
        {
            return PartialView(RoleDataAccess.GetDetailRoleById(paramId));
        }



        //DELETE EMPLOYEE
        [HttpPost]
        public JsonResult DeleteDataRole(int paramId)
        {
            string latestCode = RoleDataAccess.DeleteRole(paramId);

            if (latestCode != "")
            {
                return Json(new { success = true, latestCode, message = RoleDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = RoleDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult AutoCompleteRoleName(string prefix)
        {
            var item = RoleDataAccess.SearchStringRoleName(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AutoCompleteRoleCode(string prefix)
        {
            var item = RoleDataAccess.SearchStringRoleCode(prefix);

            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}