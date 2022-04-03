using ESOL_BO.DbAccess;
using ESOL_BO.DbAccess.Security;
using ESOL_EDU.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESOL_EDU.Controllers
{
    public class HomeController : Controller
    {
        private RolePermissionDao rolePermissionDao = new RolePermissionDao();

        public ActionResult Index()
        {
            return View(rolePermissionDao.GetUserWiseApp(AppHelper.OrgId, AppHelper.RoleId, AppHelper.UserName));
        }
        public ActionResult Dashboard(int? id)
        {
            List<UserMenuDomain> menuList = rolePermissionDao.GetUserWiseMenu(AppHelper.OrgId, id, AppHelper.UserName);
            Session["MenuList"] = menuList;
            Session["AppId"] = id;
            ViewBag.BrowserInfo = Request.Browser.Browser;
            ViewBag.IpInfo = Request.UserHostAddress;
            return View();
        }
        public PartialViewResult LoadMenu()
        {
            List<UserMenuDomain> _List = Session["MenuList"] as List<UserMenuDomain>;
            return PartialView("_PartialLeftMenu", _List);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetDefaultShowroom()
        {
            return Json(AppHelper.BranchId, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoleId()
        {
            return Json(AppHelper.RoleId, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetDefaultShowroom(int id)
        {
            UserDomain userInfo = new UserDomain();
            userInfo = Session["UserInfo"] as UserDomain;
            userInfo.BranchId = id;
            Session["UserInfo"] = userInfo;
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetShowroom()
        {
            var list = DataAccess.ShowroomDao.GetAll(AppHelper.OrgId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}