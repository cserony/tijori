using ESOL_EDU.Controllers;
using ESOL_BO.DbAccess;
using ESOL_BO.DbAccess.Security;
using ESOL_EDU.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ESOL_EDU.Areas.Security.Controllers
{
    public class UserRoleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UserRoleDomain> _List = new List<UserRoleDomain>();
            var list = DataAccess.UserRoleDao.GetByOrg(AppHelper.OrgId);
            foreach (UserRoleDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserRoleDomain entity)
        {
            try
            {
                if (entity.RoleId > 0)
                {
                    entity.StatementType = "Update";
                }
                else
                {
                    entity.StatementType = "Insert";
                }
                entity.OrgId = AppHelper.OrgId;
                entity.CreatedBy = AppHelper.CreatedBy;
                entity.CreatedDate = AppHelper.CreatedDate;
                entity.UpdatedBy = AppHelper.UpdatedBy;
                entity.UpdatedDate = AppHelper.UpdatedDate;
                DataAccess.UserRoleDao.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.RoleId > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception Ex)
            {
                return View(Ex);
            }
        }
        public ActionResult Edit(int id)
        {
            var list = DataAccess.UserRoleDao.GetById(AppHelper.OrgId, id);
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("UserRole", "where OrgId='" + AppHelper.OrgId + "' and RoleId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("UserRole", "where OrgId='" + AppHelper.OrgId + "' and RoleName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}