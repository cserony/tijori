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
    public class ThanaController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ThanaDomain> _List = new List<ThanaDomain>();
            var list = DataAccess.Thana.GetByOrg("select t.*,d.DistrictName from Thana t left join District d on d.DistrictId=t.DistrictId order by d.DistrictName asc");
            foreach (ThanaDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            ViewBag.DistrictList = ESOL_BO.DbAccess.DataAccess.District.GetByOrg("select dis.*,d.DivisionName from District dis left join Division d on d.DivisionId=dis.DivisionId order by d.DivisionName asc");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ThanaDomain entity)
        {
            try
            {
                if (entity.ThanaId > 0)
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
                DataAccess.Thana.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.ThanaId > 0)
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
            var list = DataAccess.Thana.GetById("select * from Thana where ThanaId='" + id + "'");
            ViewBag.DistrictList = ESOL_BO.DbAccess.DataAccess.District.GetByOrg("select dis.*,d.DivisionName from District dis left join Division d on d.DivisionId=dis.DivisionId order by d.DivisionName asc");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("Thana", "where OrgId='" + AppHelper.OrgId + "' and ThanaId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("Thana", "where OrgId='" + AppHelper.OrgId + "' and ThanaName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}