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
    public class ShippingZoneController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceShippingZoneDomain> _List = new List<EcommerceShippingZoneDomain>();
            var list = DataAccess.EcommerceShippingZone.GetByAll();
            foreach (EcommerceShippingZoneDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            ViewBag.Division = DataAccess.Division.GetByOrg("select * from Division order by DivisionName asc");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EcommerceShippingZoneDomain entity)
        {
            try
            {
                if (entity.ShippingZoneId > 0)
                {
                    entity.StatementType = "Update";
                }
                else
                {
                    entity.StatementType = "Insert";
                }
                DataAccess.EcommerceShippingZone.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.ShippingZoneId > 0)
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
            var list = DataAccess.EcommerceShippingZone.GetById(id);
            ViewBag.Division = DataAccess.Division.GetByOrg("select * from Division order by DivisionName asc");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceShippingZone", "where ShippingZoneId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceShippingZone", "where DivisionId='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}