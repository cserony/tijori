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
    public class SubCategoryController : BaseController
    {
        CommonHelper helper = new CommonHelper();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceSubCategoryDomain> _List = new List<EcommerceSubCategoryDomain>();
            var list = DataAccess.EcommerceSubCategory.GetByOrg("select sc.*,c.CategoryName from EcommerceSubCategory sc left join EcommerceCategory c on c.CategoryId=sc.CategoryId order by sc.SubCategoryId desc");
            foreach (EcommerceSubCategoryDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            ViewBag.CategoryList = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory order by CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EcommerceSubCategoryDomain entity)
        {
            try
            {
                if (entity.SubCategoryId > 0)
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
                entity.ImageName = "";
                entity.Slug = helper.ReplaceStringForSlag(entity.SubCategoryName);
                entity.Path = "/shop/" + entity.Slug;
                entity.IsActive = true;
                DataAccess.EcommerceSubCategory.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.SubCategoryId > 0)
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
            var list = DataAccess.EcommerceSubCategory.GetById("select * from EcommerceSubCategory where SubCategoryId='" + id + "'");
            ViewBag.CategoryList = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory order by CategoryName");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceSubCategory", "where OrgId='" + AppHelper.OrgId + "' and SubCategoryId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceSubCategory", "where OrgId='" + AppHelper.OrgId + "' and SubCategoryName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}