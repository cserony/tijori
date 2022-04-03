using ESOL_EDU.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ESOL_BO.Ecommerce;
using ESOL_BO.DbAccess;
using ESOL_EDU.Helper;

namespace ESOL_EDU.Areas.Ecommerce.Controllers
{
    public class VatTypeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceVatTypeDomain> _List = new List<EcommerceVatTypeDomain>();
            var list = DataAccess.EcommerceVatType.GetByOrg("select * from EcommerceVatType");
            foreach (EcommerceVatTypeDomain item in list)
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
        public ActionResult Create(EcommerceVatTypeDomain entity)
        {
            try
            {
                if (entity.VatTypeId > 0)
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
                DataAccess.EcommerceVatType.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.VatTypeId > 0)
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
            var list = DataAccess.EcommerceVatType.GetById("select * from EcommerceVatType where VatTypeId='" + id + "'");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceVatType", "where OrgId='" + AppHelper.OrgId + "' and VatTypeId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceVatType", "where OrgId='" + AppHelper.OrgId + "' and VatTypeName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}