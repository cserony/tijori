using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESOL_EDU.Controllers
{
    public class ThankYouController : Controller
    {
        public ActionResult Index()
        {
            Session["OrderDetails"] = null;
            return View();
        }
    }
}