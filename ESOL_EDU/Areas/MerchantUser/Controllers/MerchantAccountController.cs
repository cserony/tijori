using ESOL_EDU.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESOL_BO.DbAccess;
using ESOL_BO.Merchant;
using ESOL_EDU.Helper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ESOL_EDU.Areas.MerchantUser.Controllers
{
    public class MerchantAccountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<MerchantUserDomain> _List = new List<MerchantUserDomain>();
            var list = DataAccess.MerchantUser.GetByAll(AppHelper.OrgId);
            foreach (MerchantUserDomain item in list)
            {
                item.Date = item.CreatedDate.ToString("dd-MMM-yyyy");
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult View(int id)
        {
            MerchantUserDomain list = DataAccess.MerchantUser.GetById(AppHelper.OrgId,id);
            return View(list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("MerchantAccount", "where OrgId='" + AppHelper.OrgId + "' and MerchantId='" + id + "'");
            return RedirectToAction("Index");
        }
        public ActionResult Photo(string studentPho)
        {
            var photo = DataAccess.MerchantUser.ViewPhoto(AppHelper.OrgId,studentPho);
            if (photo == null)
            {
                return File(Server.MapPath("~/Images/photo.jpg"), "image/png");
            }
            return File(photo, "image/png");
        }
    }
}