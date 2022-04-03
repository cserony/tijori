using ESOL_EDU.Controllers;
using ESOL_BO.DbAccess;
using ESOL_BO.DbAccess.Security;
using ESOL_EDU.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESOL_EDU.Areas.Security.Controllers
{
    public class RolePermissionController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.AppList = DataAccess.RolePermissionDao.GetUserWiseApp(AppHelper.OrgId, AppHelper.RoleId, AppHelper.UserName);
            ViewBag.RoleList = DataAccess.RolePermissionDao.GetChieldRole(AppHelper.OrgId, AppHelper.RoleId);
            ViewBag.ModuleList = new List<UserModuleDomain>();
            return View();
        }
        [HttpPost]
        public ActionResult Create(List<RolePermissionDomain> lists)
        {
            try
            {
                foreach (RolePermissionDomain item in lists)
                {
                    if (item.IsCreate || item.IsEdit || item.IsDelete || item.IsView)
                    {
                        if (item.IsCreate || item.IsEdit || item.IsDelete)
                        {
                            item.IsView = true;
                        }
                        if (item.IsCreate) item.Create = 1;
                        else item.Create = 0;
                        if (item.IsEdit) item.Edit = 1;
                        else item.Edit = 0;
                        if (item.IsDelete) item.Delete = 1;
                        else item.Delete = 0;
                        if (item.IsView) item.View = 1;
                        else item.View = 0;
                        item.OrgId = AppHelper.OrgId;
                        DataAccess.RolePermissionDao.Save(item);
                    }

                    else if (item.RolePermissionId > 0)
                    {
                        item.OrgId = AppHelper.OrgId;
                        CommonHelper.Delete("UserRolePermission", "where OrgId='" + AppHelper.OrgId + "' and RolePermissionId='" + item.RolePermissionId + "'");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetRolePermission(string moduleId, string roleId)
        {
            var list = DataAccess.RolePermissionDao.GetRolePermission(Convert.ToInt32(moduleId), Convert.ToInt32(roleId), AppHelper.RoleId);
            ViewBag.RoleId = Convert.ToInt32(roleId);
            return View("_PartialRolePermission", list);
        }
        public string GetModule(int appId)
        {
            string result = "<option value=0>Select</option>";
            var listItem = DataAccess.RolePermissionDao.GetModule(AppHelper.OrgId, appId);
            foreach (var item in listItem)
            {
                result = result + "<option value=" + item.ModuleId + ">" + item.ModuleName + "</option>";
            }
            return result;
        }
    }
}