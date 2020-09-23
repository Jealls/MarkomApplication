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
                Session["requestDate"] = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
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


        //APPROVE EVENT
        public ActionResult ApproveEvent(int paramId) 
        {
            var stat = EventDataAccess.GetEventStatus(paramId);
            if (stat == 1)
            {
                ViewBag.EmpName = new SelectList(EventDataAccess.GetAllEmpName(), "EmpId", "EmpFullName");

                return PartialView(EventDataAccess.GetDetailEventById(paramId));
            }
            else if (stat == 2)
            {
                return PartialView("CloseRequestEvent", EventDataAccess.GetDetailEventById(paramId));
            } 
            else
            {
                return PartialView();
            }
            
        }

        [HttpPost]
        public ActionResult ApproveDataEvent(EventViewModel paramAppEv)
        {
            if (ModelState.IsValid)
            {
                paramAppEv.status = 2;
                //update data
                paramAppEv.approveBy = 2;
                paramAppEv.approveDate = DateTime.Now;

                string latestCode = EventDataAccess.ApproveEv(paramAppEv);

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


        [HttpPost]
        public ActionResult RejectDataEvent(EventViewModel paramRejectEv)
        {
            if (ModelState.IsValid)
            {
                paramRejectEv.status = 0;

                string latestCode = EventDataAccess.RejectEv(paramRejectEv);

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

        [HttpPost]
        public ActionResult CloseDataEvent(EventViewModel paramCloseEv)
        {
            if (ModelState.IsValid)
            {
                paramCloseEv.status = 3;
                paramCloseEv.closedDate = DateTime.Now;

                string latestCode = EventDataAccess.CloseEv(paramCloseEv);

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



        //CLOSE EVENT
        public ActionResult CloseRequestEvent(int paramId)
        {
            //var stat = EventDataAccess.GetEventStatus(paramId);

            return PartialView(EventDataAccess.GetDetailEventById(paramId));

        }



    }
}