using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESOL_EDU.Controllers
{
    public class siteController : Controller
    {
        public ActionResult aboutus()
        {
            return View();
        }
        public ActionResult contactus()
        {
            return View();
        }
    }
}