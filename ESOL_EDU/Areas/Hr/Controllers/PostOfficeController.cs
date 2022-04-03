using ESOL_EDU.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ESOL_BO.Hr;
using ESOL_BO.DbAccess;
using ESOL_EDU.Helper;

namespace ESOL_EDU.Areas.Hr.Controllers
{
    public class PostOfficeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<PostOfficeDomain> _List = new List<PostOfficeDomain>();
            var list = DataAccess.PostOffice.GetByOrg("select po.*,t.ThanaName from PostOffice po left join Thana t on t.ThanaId=po.ThanaId order by t.ThanaName asc");
            foreach (PostOfficeDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            ViewBag.ThanaList = ESOL_BO.DbAccess.DataAccess.Thana.GetByOrg("select t.*,d.DistrictName from Thana t left join District d on d.DistrictId=t.DistrictId order by d.DistrictName asc");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostOfficeDomain entity)
        {
            try
            {
                if (entity.PostOfficeId > 0)
                {
                    entity.StatementType = "Update";
                }
                else
                {
                    entity.StatementType = "Insert";
                }
                entity.OrgId = AppHelper.OrgId;
                entity.BranchId = AppHelper.BranchId;
                entity.CreatedBy = AppHelper.CreatedBy;
                entity.CreatedDate = AppHelper.CreatedDate;
                entity.UpdatedBy = AppHelper.UpdatedBy;
                entity.UpdatedDate = AppHelper.UpdatedDate;
                entity.IsActive = true;
                DataAccess.PostOffice.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.PostOfficeId > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var list = DataAccess.PostOffice.GetById("select * from PostOffice where PostOfficeId='" + id + "'");
            ViewBag.ThanaList = ESOL_BO.DbAccess.DataAccess.Thana.GetByOrg("select t.*,d.DistrictName from Thana t left join District d on d.DistrictId=t.DistrictId order by d.DistrictName asc");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("PostOffice", "where OrgId='" + AppHelper.OrgId + "' and PostOfficeId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("PostOffice", "where OrgId='" + AppHelper.OrgId + "' and PostOfficeName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}