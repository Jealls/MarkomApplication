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

        public ActionResult AddCompany()
        {
            return PartialView();
        }
    }
}