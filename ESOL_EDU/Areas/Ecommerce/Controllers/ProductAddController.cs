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
using System.IO;

namespace ESOL_EDU.Areas.Ecommerce.Controllers
{
    public class ProductAddController : BaseController
    {
        CommonHelper helper = new CommonHelper();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceProductDomain> _List = new List<EcommerceProductDomain>();
            var list = DataAccess.EcommerceProduct.GetByOrg(AppHelper.EmpId);
            foreach (EcommerceProductDomain item in list)
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
            ViewBag.SubSubCategoryList = new List<EcommerceSubSubCategoryDomain>();
            ViewBag.ServiceType = DataAccess.EcommerceServiceType.GetByOrg("select * from EcommerceServiceType where IsActive=1 order by ServiceTypeName asc");
            ViewBag.Badge = DataAccess.EcommerceBadge.GetByOrg("select * from EcommerceBadge where IsActive=1 order by BadgeName asc");
            ViewBag.Brand = DataAccess.EcommerceBrand.GetByOrg("select * from EcommerceBrand where IsActive=1 order by BrandName asc");
            ViewBag.Color = DataAccess.EcommerceColor.GetByOrg("select * from EcommerceColor where IsActive=1 order by ColorName asc");
            ViewBag.ServiceType = DataAccess.EcommerceServiceType.GetByOrg("select * from EcommerceServiceType where IsActive=1 order by ServiceTypeName asc");
            ViewBag.Size = DataAccess.EcommerceSize.GetByOrg("select * from EcommerceSize where IsActive=1 order by SizeName asc");
            ViewBag.Unit = DataAccess.EcommerceUnit.GetByOrg("select * from EcommerceUnit where IsActive=1 order by UnitName asc");
            ViewBag.Weight = DataAccess.EcommerceWeight.GetByOrg("select * from EcommerceWeight where IsActive=1 order by WeightName asc");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EcommerceProductDomain entity)
        {
            try
            {
                if (entity.ProductId > 0)
                {
                    entity.StatementType = "Update";
                }
                else
                {
                    entity.StatementType = "Insert";
                }
                string ImgPath = "";
                entity.OrgId = AppHelper.OrgId;
                entity.BranchId = AppHelper.BranchId;
                entity.CreatedBy = AppHelper.CreatedBy;
                entity.CreatedDate = AppHelper.CreatedDate;
                entity.UpdatedBy = AppHelper.UpdatedBy;
                entity.UpdatedDate = AppHelper.UpdatedDate;
                entity.UserId = AppHelper.EmpId;
                entity.Slug = helper.ReplaceStringForSlag(entity.ProductName);
                entity.Path = "/shop/product/" + entity.Slug;

                HttpPostedFileBase filePp = Request.Files["ImagePath"];
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
                        filePp.SaveAs(Server.MapPath("~/Upload/Product/" + fullPp));
                        imagePath = "Upload/Product/" + fullPp;
                        if (entity.CategoryId > 0)
                        {
                            if (System.IO.File.Exists(Server.MapPath(entity.ImagePath)))
                            {
                                System.IO.File.Delete(Server.MapPath(entity.ImagePath));
                            }
                        }

                    }
                }
                else
                {
                    imagePath = ImgPath;
                }
                entity.ImagePath = imagePath;
                entity.IsActive = true;
                int proId = DataAccess.EcommerceProduct.InsertOrUpdate(entity);

                // stock insert
                if (proId > 0)
                {
                    EcommerceProductStockInfoDomain stock = new EcommerceProductStockInfoDomain();
                    stock.OrgId = AppHelper.OrgId;
                    stock.BranchId = AppHelper.BranchId;
                    stock.UserId = AppHelper.EmpId;
                    stock.ProductId = proId;
                    stock.Quantity = entity.Quantity;
                    stock.MRP = entity.SalePrice;
                    stock.SalesId = 0;
                    stock.ReturnId = 0;
                    stock.TransectionDate = DateTime.Now;
                    stock.TransectionType = 1; // stock in
                    stock.CreatedBy = AppHelper.CreatedBy;
                    stock.CreatedDate = AppHelper.CreatedDate;
                    stock.UpdatedBy = AppHelper.UpdatedBy;
                    stock.UpdatedDate = AppHelper.UpdatedDate;
                    stock.StatementType = "Insert";
                    DataAccess.EcommerceProductStockInfo.InsertOrUpdate(stock);
                }else
                {
                    EcommerceProductStockInfoDomain stock = new EcommerceProductStockInfoDomain();
                    stock.OrgId = AppHelper.OrgId;
                    stock.BranchId = AppHelper.BranchId;
                    stock.UserId = AppHelper.EmpId;
                    stock.ProductId = 1;
                    stock.Quantity = entity.Quantity;
                    stock.MRP = entity.SalePrice;
                    stock.SalesId = 0;
                    stock.ReturnId = 0;
                    stock.TransectionDate = DateTime.Now;
                    stock.TransectionType = 1; // stock in
                    stock.CreatedBy = AppHelper.CreatedBy;
                    stock.CreatedDate = AppHelper.CreatedDate;
                    stock.UpdatedBy = AppHelper.UpdatedBy;
                    stock.UpdatedDate = AppHelper.UpdatedDate;
                    stock.StatementType = "Insert";
                    DataAccess.EcommerceProductStockInfo.InsertOrUpdate(stock);
                }


                TempData["SaveMessage"] = "Saved successfully";
                if (entity.ProductId > 0)
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
            var list = DataAccess.EcommerceProduct.GetById(id);
            ViewBag.CategoryList = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory order by CategoryName");
            ViewBag.SubCategoryList = DataAccess.EcommerceSubCategory.GetByOrg("select sc.*,c.CategoryName from EcommerceSubCategory sc left join EcommerceCategory c on c.CategoryId=sc.CategoryId order by sc.SubCategoryId desc");
            ViewBag.SubSubCategoryList = DataAccess.EcommerceSubSubCategory.GetByOrg("select ssc.*,c.CategoryName,sc.SubCategoryName from EcommerceSubSubCategory ssc left join EcommerceCategory c on c.CategoryId=ssc.CategoryId left join EcommerceSubCategory sc on sc.SubCategoryId=ssc.SubCategoryId order by ssc.SubCategoryId desc");
            ViewBag.ServiceType = DataAccess.EcommerceServiceType.GetByOrg("select * from EcommerceServiceType where IsActive=1 order by ServiceTypeName asc");
            ViewBag.Badge = DataAccess.EcommerceBadge.GetByOrg("select * from EcommerceBadge where IsActive=1 order by BadgeName asc");
            ViewBag.Brand = DataAccess.EcommerceBrand.GetByOrg("select * from EcommerceBrand where IsActive=1 order by BrandName asc");
            ViewBag.Color = DataAccess.EcommerceColor.GetByOrg("select * from EcommerceColor where IsActive=1 order by ColorName asc");
            ViewBag.ServiceType = DataAccess.EcommerceServiceType.GetByOrg("select * from EcommerceServiceType where IsActive=1 order by ServiceTypeName asc");
            ViewBag.Size = DataAccess.EcommerceSize.GetByOrg("select * from EcommerceSize where IsActive=1 order by SizeName asc");
            ViewBag.Unit = DataAccess.EcommerceUnit.GetByOrg("select * from EcommerceUnit where IsActive=1 order by UnitName asc");
            ViewBag.Weight = DataAccess.EcommerceWeight.GetByOrg("select * from EcommerceWeight where IsActive=1 order by WeightName asc");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceProduct", "where UserId='" + AppHelper.EmpId + "' and ProductId='" + id + "'");
            CommonHelper.Delete("EcommerceProductStockInfo", "where UserId='" + AppHelper.EmpId + "' and ProductId='" + id + "'");
            return RedirectToAction("Index");
        }
        public string GetSubsubCategoryByCategory(int categoryId, int subCategoryId)
        {
            string result = "<option value=0>Select</option>";
            var listItem = helper.GetList("SubSubCategoryId", "SubSubCategoryName", "EcommerceSubSubCategory", " where OrgId='" + AppHelper.OrgId + "' and BranchId='" + AppHelper.BranchId + "' and CategoryId='" + categoryId + "' and SubCategoryId='" + subCategoryId + "'", " order by SubSubCategoryName asc");
            foreach (var item in listItem)
            {
                result = result + "<option value=" + item.Code + ">" + item.Name + "</option>";
            }
            return result;
        }
    }
}