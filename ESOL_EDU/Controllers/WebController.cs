using ESOL_BO.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESOL_EDU.Helper;
using ESOL_BO.Ecommerce;

namespace ESOL_EDU.Controllers
{
    public class WebController : Controller
    {
        public ActionResult Index()
        {
            string where = "R";
            string where1 = "A";
            ViewBag.FeaturedProduct = DataAccess.EcommerceProduct.GetProductRelated(14, where);
            ViewBag.PopularProduct = DataAccess.EcommerceProduct.GetProductRelated(15, where);
            ViewBag.NewProduct = DataAccess.EcommerceProduct.GetProductRelated(0, where1);
            ViewBag.CategoryAll = DataAccess.EcommerceCategory.CategoryAll("select c.CategoryId,c.CategoryName,c.ImageName,c.Slug,COUNT(c.CategoryId) as CatCount from EcommerceCategory c left join EcommerceProduct p on p.CategoryId=c.CategoryId group by c.CategoryId,c.CategoryName,c.ImageName,c.Slug");
            ViewBag.SliderAll = DataAccess.EcommerceHomePageSlider.GetByOrg("select top 3 * from EcommerceHomePageSlider where GETDATE() between FromDate and ToDate");
            return View();
        }
        public PartialViewResult LoadEcommerceMenu()
        {
            var _List = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory where IsActive=1 order by CategoryName asc");
            return PartialView("_PartialEcommerceMenu", _List);
        }
        public PartialViewResult LoadNews()
        {
            var _List = DataAccess.EcommerceNotice.GetByOrg("select top 1 * from EcommerceNotice order by NoticeId desc");
            return PartialView("_PartialEcommerceNews", _List);
        }
        public JsonResult Subscriber(EcommerceSubscriberDomain subscriber)
        {
            subscriber.StatementType = "Insert";
            subscriber.CreatedDate = DateTime.Now;
            return Json(DataAccess.EcommerceSubscriber.InsertOrUpdate(subscriber), JsonRequestBehavior.AllowGet);
        }
    }
}