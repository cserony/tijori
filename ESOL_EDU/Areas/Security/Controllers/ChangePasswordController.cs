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
    public class ChangePasswordController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.UserName = AppHelper.UserName;
            ViewBag.Success = TempData["Success"];
            return View();
        }
        [HttpPost]
        public ActionResult Create(ChangePasswordDomain entity)
        {
            try
            {
                entity.OrgId = AppHelper.OrgId;
                entity.BranchId = AppHelper.BranchId;
                entity.UserId = AppHelper.UserId;
                int i = DataAccess.ChangePasswordDao.ChangePassword(entity);
                if (i != 0)
                {
                    TempData["Success"] = "Password Change Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}