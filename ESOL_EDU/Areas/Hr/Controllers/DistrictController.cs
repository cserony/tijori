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
    public class DistrictController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<DistrictDomain> _List = new List<DistrictDomain>();
            var list = DataAccess.District.GetByOrg("select dis.*,d.DivisionName from District dis left join Division d on d.DivisionId=dis.DivisionId order by d.DivisionName asc");
            foreach (DistrictDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            ViewBag.DivisionList = ESOL_BO.DbAccess.DataAccess.Division.GetByOrg("select * from Division");
            return View();
        }

        [HttpPost]
        public ActionResult Create(DistrictDomain entity)
        {
            try
            {
                if (entity.DistrictId > 0)
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
                entity.IsActive = true;
                DataAccess.District.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.DistrictId > 0)
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
            var list = DataAccess.District.GetById("select * from District where DistrictId='" + id + "'");
            ViewBag.DivisionList = ESOL_BO.DbAccess.DataAccess.Division.GetByOrg("select * from Division");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("District", "where OrgId='" + AppHelper.OrgId + "' and DistrictId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("District", "where OrgId='" + AppHelper.OrgId + "' and DistrictName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}