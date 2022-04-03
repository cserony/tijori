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
    public class CouponController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceCouponDomain> _List = new List<EcommerceCouponDomain>();
            var list = DataAccess.EcommerceCoupon.GetByAll();
            foreach (EcommerceCouponDomain item in list)
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
        public ActionResult Create(EcommerceCouponDomain entity)
        {
            try
            {
                if (entity.CouponId > 0)
                {
                    entity.StatementType = "Update";
                }
                else
                {
                    entity.StatementType = "Insert";
                }
                DataAccess.EcommerceCoupon.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.CouponId > 0)
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
            var list = DataAccess.EcommerceCoupon.GetById(id);
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceCoupon", "where CouponId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceCoupon", "where CouponCode='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}