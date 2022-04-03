using ESOL_EDU.Controllers;
using ESOL_BO.DbAccess;
using ESOL_BO.DbAccess.Security;
using ESOL_EDU.Helper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESOL_EDU.Areas.Security.Controllers
{
    public class UserAddController : BaseController
    {
        public ActionResult Index()     
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UserDomain> _List = new List<UserDomain>();
            var list = DataAccess.UserInfoDao.GetByOrg(AppHelper.OrgId,AppHelper.BranchId);
            foreach (UserDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.RoleList = DataAccess.UserRoleDao.GetByOrg(AppHelper.OrgId);
            ViewBag.Message = TempData["SaveMessage"];
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserDomain entity)
        {
            try
            {
                if (entity.UserId > 0)
                {
                    entity.StatementType = "Update";
                }
                else
                {
                    entity.StatementType = "Insert";
                    entity.IsActive = true;
                }
                entity.OrgId = AppHelper.OrgId;
                entity.BranchId = AppHelper.BranchId;
                entity.CreatedBy = AppHelper.CreatedBy;
                entity.CreatedDate = AppHelper.CreatedDate;
                entity.UpdatedBy = AppHelper.UpdatedBy;
                entity.UpdatedDate = AppHelper.UpdatedDate;
                DataAccess.UserInfoDao.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.UserId > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult Edit(int id)
        {
            var list = DataAccess.UserInfoDao.GetById(AppHelper.OrgId,AppHelper.BranchId, id);
            ViewBag.RoleList = DataAccess.UserRoleDao.GetByOrg(AppHelper.OrgId);
            ViewBag.Edit = "Edit";
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("UserInfo", "where OrgId='" + AppHelper.OrgId + "' and BranchId='" + AppHelper.BranchId + "' and UserId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("UserInfo", "where OrgId='" + AppHelper.OrgId + "' and BranchId='" + AppHelper.BranchId + "' and UserName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult View(int id)
        {
            var list = DataAccess.UserInfoDao.GetById(AppHelper.OrgId,AppHelper.BranchId, id);
            return View(list);
        }
    }
}