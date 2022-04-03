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
    public class NewOrderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            string where = " where o.StatusId=3 order by o.OrderDate desc";
            List<EcommerceOrderDomain> _List = new List<EcommerceOrderDomain>();
            var list = DataAccess.EcommerceOrder.GetByAll(where);
            foreach (EcommerceOrderDomain item in list)
            {
                item.Date = item.OrderDate.ToString("dd-MMM-yyyy");
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult ReadSub(int OrderId, [DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceOrderDetailsDomain> _List = new List<EcommerceOrderDetailsDomain>();
            var list = DataAccess.EcommerceOrderDetails.GetByOrderId(OrderId);
            foreach (EcommerceOrderDetailsDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceOrder", "where OrderId='" + id + "'");
            CommonHelper.Delete("EcommerceOrderDetails", "where OrderId='" + id + "'");
            CommonHelper.Delete("EcommerceOrderRequestUpdate", "where OrderId='" + id + "'");
            CommonHelper.Delete("EcommerceRecentlyViews", "where OrderId='" + id + "'");
            return RedirectToAction("Index");
        }
        public ActionResult View(int id)
        {
            EcommerceOrderDomain list = DataAccess.EcommerceOrder.GetById(id);
            list.ItemList = DataAccess.EcommerceOrderDetails.GetByOrderId(id);
            list.Date = list.OrderDate.ToString("dd-MMM-yyyy");
            return View(list);
        }
    }
}