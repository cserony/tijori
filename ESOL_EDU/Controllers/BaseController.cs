using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ESOL_EDU.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session.IsNewSession || Session["UserInfo"] == null)
            {

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult result = Json("SessionTimeout", "text/html");
                    filterContext.Result = result;
                }
                else
                {
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(new { action = "Login", controller = "Account", area = "" }));
                }
            }

            base.OnActionExecuting(filterContext);
        }

        protected void CheckSession()
        {
            if (Session.IsNewSession || Session["UserInfo"] == null)
            {
                RedirectToAction("Login", "Account", new { area = "" });
            }
        }
    }
}