using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESOL_BO.DbAccess;
using ESOL_BO.DbAccess.Security;
using ESOL_EDU.Helper;
using ESOL_EDU.Controllers;

namespace ESOL_EDU.Areas.Security.Controllers
{
    public class ReportPermissionController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.AppList = DataAccess.RolePermissionDao.GetUserWiseApp(AppHelper.OrgId, AppHelper.RoleId, AppHelper.UserName);
            ViewBag.RoleList = DataAccess.RolePermissionDao.GetChieldRole(AppHelper.OrgId, AppHelper.RoleId);
            return View();
        }
        [HttpPost]
        public ActionResult Create(ReportPermissionDomain entity, List<ReportPermissionDomain> lists)
        {
            try
            {
                CommonHelper.Delete("ReportPermission", " Where OrgId=" + AppHelper.OrgId + " and AppId=" + entity.AppId + " and RoleId=" + entity.RoleId);
                foreach (ReportPermissionDomain item in lists)
                {
                    if (item.IsPermission)
                    {
                        item.CreatedBy = AppHelper.UserName;
                        item.CreatedDate = DateTime.Today;
                        item.OrgId = AppHelper.OrgId;
                        DataAccess.RolePermissionDao.SaveReportPermission(item);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetReportList(string appId, string roleId)
        {
            var list = DataAccess.RolePermissionDao.GetReportPermission(AppHelper.OrgId,Convert.ToInt32(appId), Convert.ToInt32(roleId));
            ViewBag.RoleId = Convert.ToInt32(roleId);
            ViewBag.AppId = Convert.ToInt32(appId);
            return View("_ReportPermission", list);
        }
    }
}