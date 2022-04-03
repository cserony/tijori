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
    public class CategoryController : BaseController
    {
        CommonHelper helper = new CommonHelper();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<EcommerceCategoryDomain> _List = new List<EcommerceCategoryDomain>();
            var list = DataAccess.EcommerceCategory.GetByOrg("select * from EcommerceCategory");
            foreach (EcommerceCategoryDomain item in list)
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
        public ActionResult Create(EcommerceCategoryDomain entity)
        {
            try
            {
                if (entity.CategoryId > 0)
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
                entity.Slug = helper.ReplaceStringForSlag(entity.CategoryName);
                entity.Path = "/shop/" + entity.Slug;
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
                        filePp.SaveAs(Server.MapPath("~/Upload/Category/" + fullPp));
                        imagePath = "Upload/Category/" + fullPp;
                        if (entity.CategoryId > 0)
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
                entity.IsActive = true;
                DataAccess.EcommerceCategory.InsertOrUpdate(entity);
                TempData["SaveMessage"] = "Saved successfully";
                if (entity.CategoryId > 0)
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
            var list = DataAccess.EcommerceCategory.GetById("select * from EcommerceCategory where CategoryId='" + id + "'");
            return View("Create", list);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteByString(int id)
        {
            CommonHelper.Delete("EcommerceCategory", "where OrgId='" + AppHelper.OrgId + "' and CategoryId='" + id + "'");
            return RedirectToAction("Index");
        }
        public int CheckName(string name)
        {
            try
            {
                var list = CommonHelper.CheckName("EcommerceCategory", "where OrgId='" + AppHelper.OrgId + "' and CategoryName='" + name + "'");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}