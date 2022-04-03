using ESOL_BO.Ecommerce;
using ESOL_EDU.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ESOL_BO.DbAccess;
using ESOL_EDU.Helper;

namespace ESOL_EDU.Areas.Ecommerce.Controllers
{
    public class RecentlyViewsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceRecentlyViewsDomain> _List = new List<EcommerceRecentlyViewsDomain>();
            var list = DataAccess.EcommerceRecentlyViews.GetByAll();
            foreach (EcommerceRecentlyViewsDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceRecentlyViews", "where CustomerId='" + id + "'");
            return RedirectToAction("Index");
        }
    }
}