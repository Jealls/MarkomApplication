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

        //  ADD EMPLOYEE
        public ActionResult AddEvent()
        {
            return PartialView();
        }

    }
}