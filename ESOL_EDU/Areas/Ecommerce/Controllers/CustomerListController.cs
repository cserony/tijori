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
    public class CustomerListController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceCustomerDomain> _List = new List<EcommerceCustomerDomain>();
            var list = DataAccess.EcommerceCustomer.GetByOrg("select * from EcommerceCustomer order by CustomerId desc");
            foreach (EcommerceCustomerDomain item in list)
            {
                item.Date = item.CreatedDate.ToString("dd-MMM-yyyy");
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceCustomer", "where OrgId='" + AppHelper.OrgId + "' and CustomerId='" + id + "'");
            return RedirectToAction("Index");
        }
        public ActionResult View(int id)
        {
            var list = DataAccess.EcommerceCustomer.GetById("select * from EcommerceCustomer where CustomerId='" + id + "'");
            return View(list);
        }
    }
}