using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult ListEvent()
        {
            return View();
        }


        public ActionResult Index(EventViewModel paramSearch)
        {
            if (paramSearch.statusName != null) {
                paramSearch.statusName = paramSearch.statusName.Trim().ToLower();
            }
            List<EventViewModel> listSearchEvent = EventDataAccess.GetListEvent(paramSearch);
            return PartialView(listSearchEvent);
        }

    //  ADD EVENT
    public ActionResult AddEvent()
        {
            int empId = 1;
            Session["requestByName"] = EventDataAccess.GetEmployeeName(empId);
            if (Session["requestByName"] != null) {
                Session["requestBy"] = 1;
            }
            Session["requestDate"] = DateTime.Now;
            return PartialView();
        }


    [HttpPost]
    public ActionResult CreateDataEvent(EventViewModel paramAddEvent)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddEvent.isDelete = false;
                //status default value
                paramAddEvent.status = 1;
                //update data manual createby and createdate
                paramAddEvent.createBy = "Anastasia";
                paramAddEvent.createDate = DateTime.Now;

                    string latestCode = EventDataAccess.CreateEvent(paramAddEvent);

                    if (latestCode != "" )
                    {
                        return Json(new { success = true, latestCode, message = EventDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = EventDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        //EDIT EVENT
    public ActionResult EditEvent(int paramId)
    {
        return PartialView(EventDataAccess.GetDetailEventById(paramId));
    }

        [HttpPost]
        public ActionResult EditDataEvent(EventViewModel paramEditEv)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditEv.updateBy = "Tian";
                paramEditEv.updateDate = DateTime.Now;

                string latestCode = EventDataAccess.UpdateEvent(paramEditEv);

                if (latestCode != "")
                {
                    return Json(new { success = true, latestCode, message = EventDataAccess.Message }, JsonRequestBehavior.AllowGet);
               }
               else
               {
                   return Json(new { success = false, message = EventDataAccess.Message }, JsonRequestBehavior.AllowGet);
               }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }




    }
}