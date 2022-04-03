using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ESOL_EDU.Controllers;
using ESOL_BO.DbAccess;
using ESOL_BO.DbAccess.Security;
using ESOL_EDU.Helper;

namespace ESOL_EDU.Areas.Security.Controllers
{
    public class ShowroomController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ShowroomDomain> _List = new List<ShowroomDomain>();
            var list = DataAccess.ShowroomDao.GetAll(AppHelper.OrgId);
            foreach (ShowroomDomain item in list)
            {
                item.Date = item.CreatedDate.ToString("dd-MMM-yyyy");
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
        public ActionResult Create(ShowroomDomain entity)
        {
            try
            {
                if (entity.BranchId > 0)
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
                DataAccess.ShowroomDao.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.BranchId > 0)
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
                throw ex;
            }
        }
        public ActionResult Edit(int id)
        {
            var list = DataAccess.ShowroomDao.GetById(id);
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("Branch", "where OrgId='" + AppHelper.OrgId + "' and RoleId='" + id + "'");
            return RedirectToAction("Index");
        }
    }
}
