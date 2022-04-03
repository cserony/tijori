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
    public class SubSubCategoryController : BaseController
    {
        public CommonHelper helper = new CommonHelper();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceSubSubCategoryDomain> _List = new List<EcommerceSubSubCategoryDomain>();
            var list = DataAccess.EcommerceSubSubCategory.GetByOrg("select ssc.*,c.CategoryName,sc.SubCategoryName from EcommerceSubSubCategory ssc left join EcommerceCategory c on c.CategoryId=ssc.CategoryId left join EcommerceSubCategory sc on sc.SubCategoryId=ssc.SubCategoryId order by ssc.SubCategoryId desc");
            foreach (EcommerceSubSubCategoryDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            ViewBag.Message = TempData["SaveMessage"];
            ViewBag.CategoryList = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory order by CategoryName");
            ViewBag.SubCategoryList = new List<EcommerceSubCategoryDomain>();
            return View();
        }

        [HttpPost]
        public ActionResult Create(EcommerceSubSubCategoryDomain entity)
        {
            try
            {
                if (entity.SubSubCategoryId > 0)
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
                entity.Slug = helper.ReplaceStringForSlag(entity.SubSubCategoryName);
                entity.Path = "/shop/" + entity.Slug;
                entity.IsActive = true;
                DataAccess.EcommerceSubSubCategory.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.SubSubCategoryId > 0)
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
            var list = DataAccess.EcommerceSubSubCategory.GetById("select * from EcommerceSubSubCategory where SubSubCategoryId='" + id + "'");
            ViewBag.CategoryList = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory order by CategoryName");
            ViewBag.SubCategoryList = DataAccess.EcommerceSubCategory.GetByOrg("select sc.*,c.CategoryName from EcommerceSubCategory sc left join EcommerceCategory c on c.CategoryId=sc.CategoryId order by sc.SubCategoryId desc");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceSubSubCategory", "where OrgId='" + AppHelper.OrgId + "' and SubSubCategoryId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceSubSubCategory", "where OrgId='" + AppHelper.OrgId + "' and SubSubCategoryName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetsubCategoryByCategory(int categoryId)
        {
            string result = "<option value=0>Select</option>";
            var listItem = helper.GetList("SubCategoryId", "SubCategoryName", "EcommerceSubCategory", " where OrgId='" + AppHelper.OrgId + "' and BranchId='" + AppHelper.BranchId + "' and CategoryId='" + categoryId + "'", " order by SubCategoryName asc");
            foreach (var item in listItem)
            {
                result = result + "<option value=" + item.Code + ">" + item.Name + "</option>";
            }
            return result;
        }
    }
}