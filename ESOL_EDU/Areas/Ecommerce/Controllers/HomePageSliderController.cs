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
using System.IO;

namespace ESOL_EDU.Areas.Ecommerce.Controllers
{
    public class HomePageSliderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceHomePageSliderDomain> _List = new List<EcommerceHomePageSliderDomain>();
            var list = DataAccess.EcommerceHomePageSlider.GetByOrg("select * from EcommerceHomePageSlider");
            foreach (EcommerceHomePageSliderDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Merchant = DataAccess.MerchantUser.GetByAll(AppHelper.OrgId);
            ViewBag.Message = TempData["SaveMessage"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(EcommerceHomePageSliderDomain entity)
        {
            try
            {
                if (entity.SliderId > 0)
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
                entity.OrderBy = 1;
                entity.IsActive = true;
                string ImgPath = "";
                HttpPostedFileBase filePp = Request.Files["ImageName"];
                string extPp = Path.GetExtension(filePp.FileName);
                string imagePath = string.Empty;
                if (filePp != null && !string.IsNullOrEmpty(filePp.FileName))
                {
                    string fileNamePp = filePp.FileName;
                    string fullPp = string.Empty;

                    if (!string.IsNullOrEmpty(fileNamePp))
                    {
                        int j = fileNamePp.Split('.').Length - 1;
                        string fileExPp = fileNamePp.Split('.')[j];
                        fileNamePp = Guid.NewGuid().ToString();
                        fullPp = fileNamePp + "." + fileExPp;
                        filePp.SaveAs(Server.MapPath("~/Upload/Slider/" + fullPp));
                        imagePath = "Upload/Slider/" + fullPp;
                        if (entity.SliderId > 0)
                        {
                            if (System.IO.File.Exists(Server.MapPath(entity.ImageName)))
                            {
                                System.IO.File.Delete(Server.MapPath(entity.ImageName));
                            }
                        }

                    }
                }
                else
                {
                    imagePath = ImgPath;
                }
                entity.ImageName = imagePath;
                DataAccess.EcommerceHomePageSlider.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.SliderId > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Edit(int id)
        {
            var list = DataAccess.EcommerceHomePageSlider.GetById("select * from EcommerceHomePageSlider where SliderId='" + id + "'");
            ViewBag.Merchant = DataAccess.MerchantUser.GetByAll(AppHelper.OrgId);
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceHomePageSlider", "where OrgId='" + AppHelper.OrgId + "' and SliderId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceHomePageSlider", "where OrgId='" + AppHelper.OrgId + "' and SliderName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}