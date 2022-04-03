using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ESOL_EDU.Controllers;
using ESOL_BO.DbAccess;
using ESOL_EDU.Helper;
using ESOL_BO.DbAccess.Security;

namespace ESOL_EDU.Areas.Security.Controllers
{
    public class CompanyController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<CompanyDomain> _List = new List<CompanyDomain>();
            var list = DataAccess.CompanyDao.GetAll();
            foreach (CompanyDomain item in list)
            {
                _List.Add(item);
            }
            return Json(_List.ToDataSourceResult(request));
        }
        public ActionResult View(int id)
        {
            CompanyDomain list = new CompanyDomain();
            list = DataAccess.CompanyDao.GetById(id);
            return View(list);
        }
        public ActionResult Edit(int id)
        {
            var list = DataAccess.CompanyDao.GetById(id);
            return View(list);
        }
        [HttpPost]
        public ActionResult Edit(CompanyDomain entity)
        {
            try
            {
                entity.UpdatedBy = AppHelper.UserName;
                entity.UpdatedDate = AppHelper.UpdatedDate;
                DataAccess.CompanyDao.Update(entity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}